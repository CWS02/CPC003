﻿using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace CPC02.Function
{
    public static class WordRender
    {
        static void ReplaceParserTag(this OpenXmlElement elem, Dictionary<string, string> data)
        {
            var pool = new List<Run>();
            var matchText = string.Empty;
            var hiliteRuns = elem.Descendants<Run>() //找出鮮明提示
                .Where(o => o.RunProperties?.Elements<Highlight>().Any() ?? false).ToList();

            foreach (var run in hiliteRuns)
            {
                var t = run.InnerText;
                if (t.StartsWith("["))
                {
                    pool = new List<Run> { run };
                    matchText = t;
                }
                else
                {
                    matchText += t;
                    pool.Add(run);
                }
                if (t.EndsWith("]"))
                {
                    var m = Regex.Match(matchText, @"\[\$(?<n>\w+)\$\]");
                    if (m.Success && data.ContainsKey(m.Groups["n"].Value))
                    {
                        var firstRun = pool.First();
                        firstRun.RemoveAllChildren<Text>();
                        firstRun.RunProperties.RemoveAllChildren<Highlight>();
                        var newText = data[m.Groups["n"].Value];
                        var firstLine = true;
                        foreach (var line in Regex.Split(newText, @"\\n"))
                        {
                            if (firstLine) firstLine = false;
                            else firstRun.Append(new Break());
                            firstRun.Append(new Text(line));
                        }
                        pool.Skip(1).ToList().ForEach(o => o.Remove());
                    }
                }

            }
        }

        public static byte[] GenerateDocx(byte[] template, Dictionary<string, string> data)
        {
            using (var ms = new MemoryStream())
            {
                ms.Write(template, 0, template.Length);
                using (var docx = WordprocessingDocument.Open(ms, true))
                {
                    docx.MainDocumentPart.HeaderParts.ToList().ForEach(hdr =>
                    {
                        hdr.Header.ReplaceParserTag(data);
                    });
                    docx.MainDocumentPart.FooterParts.ToList().ForEach(ftr =>
                    {
                        ftr.Footer.ReplaceParserTag(data);
                    });
                    docx.MainDocumentPart.Document.Body.ReplaceParserTag(data);
                    docx.Save();
                }
                return ms.ToArray();
            }
        }

        //測試pdf
        public static bool ConvertDocToPdf(string workDir, string docFileName)
        {
            string openOfficePath = "C:\\Program Files\\LibreOffice\\program\\soffice.exe";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                FileName = openOfficePath,
                WorkingDirectory = workDir,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = $"--headless --convert-to pdf \"{docFileName}\" --outdir \"{workDir}\"",
                UseShellExecute = true,
                Verb = "runas"
            }; 

            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit(); 
                }

                return true; 
            }
            catch (Exception ex)
            {
                return false; 
            }
        }


    }
}