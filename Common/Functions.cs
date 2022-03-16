using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.IO;

using Fabrikafa;

namespace Fabrikafa.Common
{
    public class Functions
    {
        //string fullchars =      "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        static string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKMNPQRSTUVWXY345689";
        static string allowedNonAlphaNum = "!@#$%^&*()_-+=[{]};:<>|./?";

        /// <summary>
        /// string emailler = ";ozan@abc.com;deneme@triodor.eu,asdfsf@gmail.com,abc@xyz.com;test@gmail.com,asd@hotmail.com,";
        /// List<string> resultList = SplitEMailAddresses(emailler);
        /// noktali virgul ve/veya virgulle ayrilmis metinleri liste olarak dondurur
        /// </summary>
        /// <param name="emailAddresses"></param>
        /// <returns></returns>
        public static List<string> SplitEMailAddresses(string emailAddresses)
        {
            List<string> resultListStepOne = new List<string>();
            List<string> resultListStepTwo = new List<string>();
            List<string> resultList = new List<string>();

            //step one: split semicolon seperated ones
            string[] emailArray = emailAddresses.Replace(" ", "").Split(';');

            foreach (string item in emailArray)
            {
                //add non-empty strings as step one result
                if (!string.IsNullOrEmpty(item))
                {
                    resultListStepOne.Add(item);
                }
            }

            //step two: try to split strings seperated with coma in resultListStepOne.
            foreach (string item in resultListStepOne)
            {
                emailArray = item.Split(',');

                foreach (string item2 in emailArray)
                {
                    //add non-empty strings as step one result
                    if (!string.IsNullOrEmpty(item2))
                    {
                        resultListStepTwo.Add(item2);
                    }
                }
            }

            //step three: try to split strings seperated with enter in resultListStepTwo.
            foreach (string item in resultListStepTwo)
            {
                emailArray = item.Split('\n');

                foreach (string item2 in emailArray)
                {
                    //add non-empty strings as step one result
                    if (!string.IsNullOrEmpty(item2))
                    {
                        resultList.Add(item2);
                    }
                }
            }

            return resultList;
        }

        public static string BindXslData(string XmlData, string XslFile)
        {
            string body = string.Empty;

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(XmlData);

            System.Xml.Xsl.XslCompiledTransform xsl = new System.Xml.Xsl.XslCompiledTransform();
            xsl.Load(XslFile);

            System.Xml.XPath.XPathNavigator xpath = doc.CreateNavigator();
            StringBuilder xmlOutput = new StringBuilder();
            System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(xmlOutput);

            xsl.Transform(xpath, writer);

            writer.Close();

            body = xmlOutput.ToString();

            return body;
        }

        /// <summary>
        /// convert 127.0.0.1 to 2130706433.0
        /// </summary>
        /// <param name="IPV4"></param>
        /// <returns></returns>
        public static double IPV4ToLongIP(string IPV4)
        {
            int i;
            string[] arrDec;
            double num = 0;

            if (IPV4 == "")
            {
                return 0;
            }

            arrDec = IPV4.Split('.');

            for (i = arrDec.Length - 1; i >= 0; i--)
            {
                num += ((int.Parse(arrDec[i]) % 256) * Math.Pow(256, (3 - i)));
            }

            return num;
        }

        /// <summary>
        /// Returns a node value from given xml data
        /// </summary>
        /// <param name="SourceXML">xml data</param>
        /// <param name="XPathToNode">xpath to read node value like "/rss/channel/item"</param>
        /// <returns>node value</returns>
        public static string ReadXMLNode(string SourceXML, string XPathToNode)
        {
            XmlDocument mainXmlDoc = new XmlDocument();
            mainXmlDoc.Load(new StringReader(SourceXML));
            XmlNodeList xmlNodes = mainXmlDoc.SelectNodes(XPathToNode);

            if (xmlNodes.Count != 1)
            {
                throw new Exception("invalid or no xml node");
            }

            XmlNode node = xmlNodes[0];
            string value = string.Empty;
            value = node.InnerText;

            return value;
        }

        public static int RandomNumber(int Low, int High)
        {
            //check RandomNumberGenerator in System.Security.Cryptography
            Random rndNum = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            int rnd = rndNum.Next(Low, High);
            return rnd;
        }

        public static string GeneratePassword(int length)
        {
            string res = "";
            Random rnd = new Random();
            while (0 < length--)
                res += allowedChars[rnd.Next(allowedChars.Length)];
            return res;
        }

        [Obsolete("This method is not tested")]
        private static string GeneratePassword(int Lenght, int NonAlphaNumericChars)
        {
            Random rd = new Random();

            if (NonAlphaNumericChars > Lenght || Lenght <= 0 || NonAlphaNumericChars < 0)
                throw new ArgumentOutOfRangeException();

            char[] pass = new char[Lenght];
            int[] pos = new int[Lenght];
            int i = 0, j = 0, temp = 0;
            bool flag = false;

            //Random the position values of the pos array for the string Pass
            while (i < Lenght - 1)
            {
                j = 0;
                flag = false;
                temp = rd.Next(0, Lenght);
                for (j = 0; j < Lenght; j++)
                    if (temp == pos[j])
                    {
                        flag = true;
                        j = Lenght;
                    }

                if (!flag)
                {
                    pos[i] = temp;
                    i++;
                }
            }

            //Random the AlphaNumericChars
            for (i = 0; i < Lenght - NonAlphaNumericChars; i++)
                pass[i] = allowedChars[rd.Next(0, allowedChars.Length)];

            //Random the NonAlphaNumericChars
            for (i = Lenght - NonAlphaNumericChars; i < Lenght; i++)
                pass[i] = allowedNonAlphaNum[rd.Next(0, allowedNonAlphaNum.Length)];

            //Set the sorted array values by the pos array for the rigth posistion
            char[] sorted = new char[Lenght];
            for (i = 0; i < Lenght; i++)
                sorted[i] = pass[pos[i]];

            string Pass = new String(sorted);

            return Pass;
        }

        public static string GenerateSlug(int length)
        {
            string allowedCharsURL = "abcdefghijklmnopqrstuvwxyz1234567890";
            //$-_.+!*'(),

            //unwise in URI      = "{" | "}" | "|" | "\" | "^" | "[" | "]" | "`"
            //reserved in URI    = ";" | "/" | "?" | ":" | "@" | "&" | "=" | "+" | "$" | ","

            string res = "";
            Random rnd = new Random();
            while (0 < length--)
                res += allowedCharsURL[rnd.Next(allowedCharsURL.Length)];
            return res;
        }

        public static bool IsValidEmail(string Email, string RegExPattern)
        {
            if (string.IsNullOrEmpty(RegExPattern))
            {
                throw new Exception("No email validation pattern assigned. Please set Email ValidationPattern application setting property.");
            }

            bool retVal = false;
            retVal = System.Text.RegularExpressions.Regex.IsMatch(Email, RegExPattern);
            return retVal;
        }

        [Obsolete("Please use ToKiloFormat extension method for strings, ints and dobules")]
        public static string GetNumberForDisplay(double Number)
        {
            return Number.ToKiloFormat();
        }

        /// <summary>
        /// Clips a long text to desired lenth with desired clip mode and returns
        /// </summary>
        /// <param name="TextToClip">Soruce text to clip</param>
        /// <param name="ClipMode">Defines how to clip. </param>
        /// <param name="ResultLength">Length of the clipped text.</param>
        /// <returns></returns>
        public static string ClipText(string TextToClip, ClipModeEnum ClipMode, int ResultLength = 255)
        {
            var clippedText = string.Empty;
            var lengthTextToClip = TextToClip.Length;
            var defaultIndex = 0;
            var startIndex = defaultIndex;

            //if input text is not long enough to clip do nothing
            if (lengthTextToClip > ResultLength)
            {
                const string clipIndicator = "...";
                int lengthClipIndicator = clipIndicator.Length;
                int lengthNet = ResultLength - lengthClipIndicator;
                int lengthHalf = lengthNet / 2;

                switch (ClipMode)
                {
                    case ClipModeEnum.ClipFromStart:
                        clippedText = $"{TextToClip.Substring(defaultIndex, lengthNet)}...";
                        break;
                    case ClipModeEnum.ClipFromEnd:
                        startIndex = lengthTextToClip - lengthNet;
                        clippedText = $"...{TextToClip.Substring(startIndex)}";
                        break;
                    case ClipModeEnum.ClipFromMiddle:
                        string startPart = string.Empty;
                        string endPart = string.Empty;
                        startPart = TextToClip.Substring(defaultIndex, lengthHalf);

                        //integer division correction to return text in result length
                        var lengthHalfCorrection = 0;
                        lengthHalfCorrection = ((lengthHalf * 2 + lengthClipIndicator) < ResultLength) ? 1 : 0;

                        startIndex = lengthTextToClip - lengthHalf - lengthHalfCorrection;
                        endPart = TextToClip.Substring(startIndex, lengthHalf + lengthHalfCorrection);
                        clippedText = $"{startPart}...{endPart}";
                        break;
                    default:
                        clippedText = $"{TextToClip.Substring(defaultIndex, lengthNet)}...";
                        break;
                }
            }
            else
            {
                clippedText = TextToClip;
            }

            return clippedText;
        }

        public enum ClipModeEnum
        {
            ClipFromStart = 10,
            ClipFromEnd = 20,
            ClipFromMiddle = 30
        }
    }
}
