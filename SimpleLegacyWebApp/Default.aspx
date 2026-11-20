<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SimpleLegacyWebApp._Default" %>

<%@ Register Assembly="Atalasoft.dotImage.WebControls" Namespace="Atalasoft.Imaging.WebControls.Annotations"
    TagPrefix="cc2" %>
<%@ Register Assembly="Atalasoft.dotImage.WebControls" Namespace="Atalasoft.Imaging.WebControls"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <title></title>
    </head>
    <body>
        <form id="form1" runat="server">
        <div>
        <table>
            <tr>
                <td>
                    Select File...
                    <select id="Select1" name="D1">
                        <option value="DocCleanMultipage.tif">DocCleanMultipage.tif</option>
                        <option value="GettysburgAddress.tif">GettysburgAddress.tif</option>
                        <option value="Tiger.jpg">Tiger.jpg</option>
                        <option value="Owl.png">Owl.png</option>
                    </select>
                    <input id="btn_Open" type="button" onclick="OpenFile(Select1.value); return false;"
                            name="btn_Open" title="Open" value="Open" />
                </td>
                <td>
                    <input id="btn_YellowRectangle" type="button" onclick="CreateAnnotation('RectangleData', 'DefaultYellowRect'); return false;"
                        name="btn_YellowRectangle" title="Rectangle" value="Rectangle" />
                    <input id="btn_TextAnno" type="button" onclick="CreateAnnotation('TextData', 'DefaultText'); return false;"
                        name="btn_TextAnno" title="Text" value="Text" />
                        <input id="btn_AnnoFill"  type="button" onclick="ChangeAnnoFill(); return false;"
                        name="btn_AnnoFill" title="ChangeFill" value="Change Fill" />
                        <input id="btn_AnnoText"  type="button" onclick="ChangeAnnoText('fnord'); return false;"
                        name="btn_AnnoText" title="Change Text" value="Change Text" />
                </td>
            </tr>
            <tr>
                <td>
                    <cc1:WebThumbnailViewer ID="WebThumbnailViewer1" runat="server" Width="200px" Height="500px"
                        ViewerID="WebAnnotationViewer1" ShowAnnotations="true" />
                </td>
                <td>
                    <cc2:WebAnnotationViewer ID="WebAnnotationViewer1" runat="server" Width="750px" Height="500px"
                        AutoLinkThumbnailViewer="true" selection-width="0" selection-height="0" selection-animated="False" />
                </td>
            </tr>
        </table>
        </div>
        </form>
        <!-- the JavaScript loaded here will include the atalaClientInitScript() 
            and should be used for all JavaScript that will interact with the Atalasoft Controls  -->
        <script type="text/javascript" src="JScript1.js"></script>
    </body>
</html>
