using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Metron;
using Android.Media;
using MetronXamarin.Droid;
using Android.Text;
using System.Xml;
using Android.Content.Res;
using Android.Support.V7.View;


using System.Xml.Linq;


[assembly: Xamarin.Forms.Dependency(typeof(AndroidResourceXMLReader))]

namespace MetronXamarin.Droid
{
    public class AndroidResourceXMLReader :  IPlatformXamarinSpecificXMLDoc
    {

        XDocument IPlatformXamarinSpecificXMLDoc.GetXMLDoc()
        {

            Context context = Application.Context;
            Resources res = context.Resources;
            XmlReader xmlReader = res.GetXml(Resource.Xml.tempos_edited);

            var xdoc = XDocument.Load(xmlReader);

            return xdoc;
        }
    }
}