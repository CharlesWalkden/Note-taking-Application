using Note_taking_Application.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Note_taking_Application.Models
{
    public class ResizeAdorner : Adorner
    {
        /// <summary>
        /// Element that can resize.  
        /// </summary>
        public FrameworkElement adornedElement;

        /// <summary>
        /// Min height allowed for adornedElement.
        /// </summary>
        public double minHeightRR;

        /// <summary>
        /// Min width allowed for adornedElement.
        /// </summary>
        public double minWidthRR;


        public Thumb bottomRight = new Thumb();

        /// <summary>
        /// To store and manage the adorner's visual children.
        /// </summary>
        VisualCollection visualChildren;

        public ResizeAdorner(FrameworkElement _adornedElement, double _minHeight, double _minWidth) : base(_adornedElement)
        {
            minHeightRR = _minHeight;
            minWidthRR = _minWidth;

            visualChildren = new VisualCollection(this);

            adornedElement = _adornedElement;
            
            AdornerLayer aLayer = AdornerLayer.GetAdornerLayer(adornedElement);
            aLayer.Add(this);

            BuildAdornerCorner();

            bottomRight.DragDelta += new DragDeltaEventHandler(HandleResizing);

            HandleResizing(bottomRight, new DragDeltaEventArgs(0, 0));
        }

        public void HandleResizing(object sender, DragDeltaEventArgs args)
        {
            Thumb? hitThumb = sender as Thumb;

            if (hitThumb != null)
            {
                var heightResize = Math.Max(adornedElement.RenderSize.Height + args.VerticalChange, hitThumb.Height);
                var widthResize = Math.Max(adornedElement.RenderSize.Width + args.HorizontalChange, hitThumb.Width);

                adornedElement.Height = Math.Max(heightResize, minHeightRR);
                adornedElement.Width = Math.Max(widthResize, minWidthRR);
            }

        }

        public void PositionThumb()
        {
            var elemHeight = adornedElement.RenderSize.Height;
            var elemWidth = adornedElement.RenderSize.Width;

            //Places thumb in element's lower right corner.
            bottomRight.Arrange(new Rect(
                (elemWidth - bottomRight.Height) / 2, 
                (elemHeight - bottomRight.Width) / 2, 
                elemWidth,      
                elemHeight    
                ));
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            PositionThumb();
     
            return finalSize;
        }

        public void BuildAdornerCorner()
        {
            bottomRight.Style = Application.Current.Resources["AdornerResize_ThumbTriangle"] as Style;

            bottomRight.Cursor = Cursors.SizeNWSE;
            bottomRight.Height = 14;
            bottomRight.Width = 14;
            bottomRight.Opacity = .5;

            visualChildren.Add(bottomRight);
        }

        protected override int VisualChildrenCount { get { return visualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return visualChildren[index]; }
    }
}
