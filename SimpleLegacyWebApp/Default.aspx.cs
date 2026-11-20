using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Atalasoft.Imaging;
using Atalasoft.Imaging.WebControls;
using Atalasoft.Annotate.UI;
using Atalasoft.Annotate;
using System.Drawing;


namespace SimpleLegacyWebApp
{
    public partial class _Default : System.Web.UI.Page
    {
        /**
         * This static constructor is where you do things that need to absolutely 
         * happen first, before any other Atalasoft components are called
         */
        static _Default()
        {
            /**
             * If you're going to use PDF files, uncomment the following 3 lines of code:
             */
            //PdfDecoder decoder = new PdfDecoder();
            ////decoder.Resolution = 300;  // Optional forcing the resolution to 300 dpi (bigger dpi == bigger memory footprint, but better quality)
            //RegisteredDecoders.Decoders.Add(decoder);

            /**
             * Other decoders can be added as well
             */
            //// This is how you'd add support for Dicom
            //RegisteredDecoders.Decoders.Add(new DicomDecoder());
            //// This is how you'd add support for Jbig2
            //RegisteredDecoders.Decoders.Add(new Jbig2Decoder());
            //// This is how you'd add support for Jpeg2000
            //RegisteredDecoders.Decoders.Add(new Jp2Decoder())
        }


        /**
         * Called once the basic page is loaded - useful for setting initial state
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeDefaultAnnotations();

            //WebAnnotationViewer1.TileSize = new Size(1024, 1024);

            // We only want to do this if it's the first time through.
            if (!Page.IsPostBack)
            {
                // Keep in mind that calls to the WebThumbnailViewer will be IGNORED if this is 
                // called by RemoteInvoke. They were here for the initial PageLoad call
                // Since we have linked the WebThumbnailViewer1 and WebAnnotationViewer1 components in markup,
                // we just have to open the document in the thumbnail viewer and select the first page
                // the components will taike care of auto loading the Annotation viewer
                WebThumbnailViewer1.OpenUrl("SampleImages/DocCleanMultipage.tif");
                WebThumbnailViewer1.SelectedIndex = 0;

                WebThumbnailViewer1.ThumbCaptionFormat = "x{0:d5}";
                WebThumbnailViewer1.AllowMultiSelect = true;

                // if you are manually controlling the zoom for the viewer somewhere else, 
                // you may need to comment this line out
                WebAnnotationViewer1.AutoZoom = AutoZoomMode.BestFit;
            }
        }

        private void InitializeDefaultAnnotations() 
        {
            // This is where you can pre-set annotations if you choose
            Atalasoft.Annotate.RectangleData yellowRect = new Atalasoft.Annotate.RectangleData();
            yellowRect.Name = "DefaultYellowRect";
            yellowRect.Outline = new Atalasoft.Annotate.AnnotationPen(System.Drawing.Color.Orange, 5);
            yellowRect.Fill = new Atalasoft.Annotate.AnnotationBrush(System.Drawing.Color.Yellow);
            WebAnnotationViewer1.Annotations.DefaultAnnotations.Add(yellowRect);

            Atalasoft.Annotate.TextData textAnno = new Atalasoft.Annotate.TextData();
            textAnno.Name = "DefaultText";
            textAnno.Text = "Default Text Annotation - Double Click To Edit";
            textAnno.Outline = new Atalasoft.Annotate.AnnotationPen(System.Drawing.Color.Blue, 5);
            textAnno.Fill = new Atalasoft.Annotate.AnnotationBrush(System.Drawing.Color.Ivory);
            textAnno.Font = new Atalasoft.Annotate.AnnotationFont("Courier New", 48f);
            textAnno.AllowEditing = true;
            WebAnnotationViewer1.Annotations.DefaultAnnotations.Add(textAnno);
        }

        [RemoteInvokable]
        public void RemoteChangeAnnoFill()
        {
            if (WebAnnotationViewer1.Annotations.SelectedAnnotations.Count > 0)
            {
                AnnotationUI rawAnno = WebAnnotationViewer1.Annotations.SelectedAnnotations[0];
                RectangleAnnotation rect = rawAnno as RectangleAnnotation;
                if (rect != null)
                {
                    rect.Fill = new AnnotationBrush(Color.Blue);
                }
                WebAnnotationViewer1.UpdateAnnotations();
                WebAnnotationViewer1.UpdateClient();
            }
        }

        [RemoteInvokable]
        public void RemoteChangeAnnoText(string newText)
        {
            if (WebAnnotationViewer1.Annotations.SelectedAnnotations.Count > 0)
            {
                AnnotationUI rawAnno = WebAnnotationViewer1.Annotations.SelectedAnnotations[0];
                TextAnnotation text = rawAnno as TextAnnotation;
                if (text != null)
                {
                    text.Text = newText;
                    text.AllowEditing = true;
                }
                WebAnnotationViewer1.UpdateAnnotations();
                WebAnnotationViewer1.UpdateClient();
            }
        }

        [RemoteInvokable]
        public string OpenFileInViewers(string fileName) 
        {
            /***********************
             * HERE is where you can manipulate the image... 
             * If the image you want to open is in your DB or on some part of your
             * FileSystem outside of the scope available to Page.MapPath(),
             * NOW would be the time to copy it to a directory visible to the Page.MapPath()
             * 
             * For the purposes of the demo, we've been given the file name, and since its
             * a simple demo, it's already sitting where we want it - we just need to append
             * the virtual path to it.
             ***********************/

            string virtualPath = "SampleImages/" + fileName;

            // if you are manually controlling the zoom for the viewer somewhere else, 
            // you may need to comment this line out
            WebAnnotationViewer1.AutoZoom = AutoZoomMode.BestFit;

            // This return value will be used by the RemoteInvoked functions back in 
            // JavaScript land if called from RemoteInvoke
            return virtualPath;
        }

    }
}
