// This script will be called when the AtalasoftControls are fully loaded and initialized
atalaInitClientScript(MyPageLoad);


/**
 * Use this to do any post-initialize Atalasoft page load calls
 *
 * Some people name this OnPageLoad(). However, that can 
 * conflict with our .NET controls, so make your own name
 */
function MyPageLoad() {
    // Set the event handler for the thumbnail index change event
    WebThumbnailViewer1.SelectedIndexChanged = IndexChanged;
}

/**
 * Uses a RemoteInvoke to tell the server-side to load a new file
 */
function OpenFile(fileName) {
    // Before we open a new file, we want to completely clear out the annoations and images
    Clear();
    
    /*
     * All arguments to be sent to the RemoteInvokable method are expected to be 
     * passed as strings in an array. 
     * 
     * HOWEVER, note that you MUST have a [RemoteInvokable] method on the client side
     *          which matches the name of the first arg here, and whose arguments exactly
     *          match the types and order sent here...
     *  if I send RemoteInvoke( 'myRemoteMethod', new Array('hello', '1', 'false'));
     *     The RemoteInvoke will attempt to call the [RemoteInvokable] method
     *         public myRemoteMethod(string "hello", int 1, bool false)
     */
    var a = new Array();
    a.push(fileName);
    
    // Note that you set the RemoteInvoked handler BEFORE you call RemoteInvoke
    WebAnnotationViewer1.RemoteInvoked = RefreshThumbnailViewer;
    // Now, we call the RemoteInvoke on the Annotation viewer
    WebAnnotationViewer1.RemoteInvoke('OpenFileInViewers', a);
}


function ChangeAnnoText(newText) {
    var args = new Array();
    args.push(newText);
    //alert("about to change fill");
    WebAnnotationViewer1.RemoteInvoked = RepaintAnnotations;
    // Now, we call the RemoteInvoke on the Annotation viewer
    WebAnnotationViewer1.RemoteInvoke('RemoteChangeAnnoText', args);
}

function ChangeAnnoFill() {
    //alert("about to change fill");
    WebAnnotationViewer1.RemoteInvoked = RepaintAnnotations;
    // Now, we call the RemoteInvoke on the Annotation viewer
    WebAnnotationViewer1.RemoteInvoke('RemoteChangeAnnoFill');
}

/// This needs to be called after a rotate to get the annos right
function RepaintAnnotations() {
    var annos = WebAnnotationViewer1.getAnnotations();
    for (var i = 0; i < annos.length; i++) {
        annos[i].Repaint();
        WebThumbnailViewer1.UpdateThumb(i);
    }
}

/**
 * Allows for creation of annotations by name using the defaults created in
 * the code-behind in the InitializeDefaultAnnotations() method
 */
function CreateAnnotation(type, name) {
    //var ann = WebAnnotationViewer1.CreateAnnotation(type, name);
    WebAnnotationViewer1.CreateAnnotation(type, name);
}


/**
* This function clears the viewer properly and completely
*/
function Clear() {
    // IMPORTANT: IE acts incorrectly if one tries to clear the control
    if (!isBrowserIE()) {
        WebAnnotationViewer1.Clear();
    }
    WebThumbnailViewer1.OpenUrl("");
}


/**
 * This function is called upon the return of most remoteInvokes... 
 *
 * It expects that your RemoteInvokable function back-server-side will 
 * return the URL to hand over to the thumbnail viewer
 */
function RefreshThumbnailViewer() {
    // This ensures that RemoteInvoked is cleared out
    WebAnnotationViewer1.RemoteInvoked = function() { };

    // We expect a valid relative path for the URL to give the ThumbnailViewer
    var newUrl = WebAnnotationViewer1.getReturnValue();

    // The url must point to a file that is available within this site/application (virtual directory is fine)
    WebThumbnailViewer1.OpenUrl(newUrl);
    // Done here, client-side, this will select the first item in the thumbnail controller
    // which will in turn cause the image to be loaded by the image viewer
    WebThumbnailViewer1.SelectThumb(0);
}


/**
 * This is a simple hanlder for the IndexChanged event on the Thumbnail Viewer
 */
function IndexChanged() {
    // Sets the BestFit zoom
    //WebAnnotationViewer1.setAutoZoom(1);
}

// This code adapted from, https://codepen.io/gapcode/pen/vEJNZN
// returns simple true for any IE version (including edge) or false for other browser
function isBrowserIE() {
    var agent = window.navigator.userAgent;

    var msie = agent.indexOf('MSIE ');
    if (msie > 0) {
        return true;
    }

    var trident = agent.indexOf('Trident/');
    if (trident > 0) {
        return true;
    }

    var edge = agent.indexOf('Edge/');
    if (edge > 0) {
        return true;
    }

    // other browser
    return false;
}