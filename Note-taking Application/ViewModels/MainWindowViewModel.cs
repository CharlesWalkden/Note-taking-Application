﻿using Note_taking_Application.DataStore;
using Note_taking_Application.Event_Args;
using Note_taking_Application.Interfaces;
using Note_taking_Application.Models;
using Note_taking_Application.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Note_taking_Application.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public CollectionList<Note> NoteList { get; set; }
        

        public IDataStore DataStore { get; set; }
        public ICommand AddNoteCommand { get; set; }

        public MainWindowViewModel()
        {

            AddNoteCommand = new RelayCommand(AddNote);
            NoteList = new CollectionList<Note>();

            DataStore = new FileStore();

            if (DataStore.Load() is List<Note> data)
                NoteList.AddNewRange(data);

            SortNoteList();
        }

        private void AddNote()
        {
            Note newNote = new();
            NoteList.Add(newNote);
            SortNoteList();
        }
        private void OpenNote(NotesPageActionRequestEventArgs e)
        {
            IDialogClient<NotesPageViewModel>? newNotesPage = WindowManager.CreateWindow<NotesPage, NotesPageViewModel>();

            if (newNotesPage != null)
            {
                newNotesPage.ViewModel = new NotesPageViewModel(e.Note);

                if (e.Requester is NoteTileViewModel noteTileViewModel)
                {
                    newNotesPage.ViewModel.OnContentChanged += noteTileViewModel.NotesPage_UpdateContent;
                    newNotesPage.OnClose += noteTileViewModel.NotesPage_OnClose;
                    newNotesPage.ViewModel.OnCreateNewNote += noteTileViewModel.NotesPage_CreateNewNote;
                }

                WindowManager.OpenWindow(newNotesPage);
            }
        }

        private void RemoveNote(NotesPageActionRequestEventArgs e)
        {
            NoteList.Remove(e.Note);
            if (e.Note.IsOpen)
                CloseNote(e);
        }
        private void CloseNote(NotesPageActionRequestEventArgs e)
        {
            WindowManager.CloseWindow(e.Note);
            e.Note.IsOpen = false;
        }
        private void SaveNote(NotesPageActionRequestEventArgs e)
        {
            //DataStore.Save<Note>(note);
        }
        private void DeleteNote(NotesPageActionRequestEventArgs e)
        {
            //DataStore.Delete<Note>(note);
        }
        private void SortNoteList()
        {
            NoteList.AddNewRange(NoteList.OrderBy(x => x.LastEdit).ToList());
        }

        public void AddNote_Request(object? sender, EventArgs e)
        {
            AddNote();
        }
        public void CloseNote_Request(object? sender, NotesPageActionRequestEventArgs e)
        {
            CloseNote(e);
        }
        public void DeleteNote_Request(object? sender, NotesPageActionRequestEventArgs e)
        {
            RemoveNote(e);
        }
        public void OpenNote_Request(object? sender, NotesPageActionRequestEventArgs e)
        {
            OpenNote(e);
        }
    }

   
}
