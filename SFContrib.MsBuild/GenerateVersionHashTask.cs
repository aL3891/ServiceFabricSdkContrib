using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SfContribTasks
{
    public class GenerateVersionHashTask : Task
    {

        public string TargetDir { get; set; }


        public override bool Execute()
        {
            SHA256Managed sha = new SHA256Managed();
            int offset = 0;

            foreach (var f in Directory.GetFiles(TargetDir, "*.dll"))
            {
                var b = File.ReadAllBytes(f);
                sha.TransformBlock(b, offset, b.Length, b, offset);
            }

            sha.TransformFinalBlock(new byte[0], offset, 0);
            File.WriteAllText(Path.Combine(TargetDir, "serviceHash.hash"), Convert.ToBase64String(sha.Hash));
            return true;
        }
    }
}
