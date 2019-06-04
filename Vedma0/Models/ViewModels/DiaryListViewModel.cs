using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vedma0.Models.Logging;

namespace Vedma0.Models.ViewModels
{
    public class DiaryListViewModel
    {
        public bool DateSort { get; set; } // значение для сортировки по возрастанию даты
        public DiaryFilter Filter {get;set;} //значение фильтра типа страницы
        public int PageNumber { get; set; } // номер страницы
        public bool HasPreviousPage { get => PageNumber > 0; } // номер страницы
        public bool HasNextPage { get => PageNumber * PagesPerSheet < Count; } // есть ли следующшая страница
        public int Count { get; set; } // число элементов во всё дневнике
        public int PagesPerSheet { get; set; } // элементов на страницу
        public IEnumerable<DiaryPage> DiaryPages { get; set; } // элементы для загрузки сраницы
        public DiaryListViewModel(IEnumerable<DiaryPage> diaryPages, int count, int pagesPerSheet, int page, bool dateSort, DiaryFilter filter = DiaryFilter.All)
        {
            DiaryPages = diaryPages;
            Count = count;
            PagesPerSheet = pagesPerSheet;
            PageNumber = page;
            Filter = filter;
            DateSort = dateSort;
        }
    }

    /// <summary>
    /// список для фильтрации дневника
    /// </summary>
    public enum DiaryFilter
    {

        [Display(Name = "все")]
        All,
        [Display(Name = "системные")]
        System,
        [Display(Name = "личные")]
        Player,
        [Display(Name = "от мастеров")]
        Master
    }
}
