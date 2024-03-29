﻿namespace PersonalMoney.Models
{
    public class Summernote
    {
        public Summernote(string iDEditor, bool loadLibrary = true)
        {
            IDEditor = iDEditor;
            this.loadLibrary = loadLibrary;
        }

        public string IDEditor { get; set; }

        public bool loadLibrary { get; set; }

        public int height { get; set; } = 120;


        public string toolbar { get; set; } = @"[
            ['style', ['italic']],
            ['font', ['bold', 'underline', 'clear', 'strikethrough']],
            ['fontsize', ['fontsize']],
            ['fontname', ['fontname']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['table', ['table']],
            ['insert', ['link', 'picture', 'video', 'elfinder']],
            ['height', ['height']],
            ['view', ['fullscreen', 'codeview', 'help']]
        ]";

    }
}
