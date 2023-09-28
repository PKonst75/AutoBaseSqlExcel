using System;
using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;
using System.Text;
using System.Reflection;

namespace AutoBaseSql
{
    class DbWord
    {
        public Word.Document worddoc;
        public Word.Application wordapp;
        public DbWord()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public bool OpenFile(string file_name)
        {
            Object wMissing = System.Reflection.Missing.Value;
            Object wTrue = true;
            Object wFalse = false;
            Object path = (Object)file_name;

            worddoc = new Word.Document();
            wordapp = new Word.Application();
            worddoc = wordapp.Documents.Open(ref path, ref wTrue, ref wTrue, ref wMissing, ref wMissing, ref wMissing, ref wMissing, ref wMissing, ref wMissing, ref wMissing, ref wMissing, ref wMissing, ref wMissing, ref wMissing, ref wMissing, ref wMissing);
            return true;
        }

        public bool ReplaceAll(string mask, string replace, Word.Application app)
        {
            // обьект диапазона, собственно какая-то часть документа
            Word.Range myRange;
            // обьект пустого параметра
            object wMissing = Type.Missing;
            // строка вида @@adress, которую будем искать в документе ворд
            object textToFind = mask;
            // чем будем заменять строку шаблона
            object replaceWith = replace;

            //типа поиска и замены
            object replaceType;
            replaceType = Word.WdReplace.wdReplaceAll;
            
            // обходим все разделы документа
            for (int i = 1; i <= app.ActiveDocument.Sections.Count; i++)
            {
                // берем всю секцию диапазоном
                myRange = app.ActiveDocument.Sections[i].Range;
                // выполняем метод поискаи  замены обьекта диапазона ворд
                myRange.Find.Execute(ref textToFind, ref wMissing, ref wMissing, ref wMissing, ref wMissing, ref wMissing, ref wMissing, ref wMissing, ref wMissing, ref replaceWith, ref replaceType, ref wMissing, ref wMissing, ref wMissing, ref wMissing);
            }
            return true;
        }
    }
}
