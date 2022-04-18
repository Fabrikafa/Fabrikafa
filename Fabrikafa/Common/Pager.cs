using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabrikafa.Common;

public class Pager
{
    /// <summary>
    /// Sample: /admin/index
    /// </summary>
    public string PagerRoute { get; set; }
    public string SearchText { get; set; }
    public int DefaultStartIndex { get; set; } = 1;
    public int CurrentPage { get; set; } = 1;
    
    public int TotalCount = 0; //this is a field rather than property to pass values from methods with out keyword
    public int PageSize { get; set; } = 10;
    public int PagerFrameSize { get; set; } = 10;
    public int XwardStepSize { get; set; } = 5;

    public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalCount, PageSize));

    /* 
     Example
     assume 27 total pages
     current page is 15
     result of integer division gives us the coefficient
     15 / 10 = 1 
    */

    int coefficient => CurrentPage / PagerFrameSize;
    int calculatedStartIndex => coefficient == 0 ? DefaultStartIndex : coefficient * PagerFrameSize;
    int calculatedEndIndex => coefficient == 0 ? PagerFrameSize : calculatedStartIndex + PagerFrameSize/* - 1*/;
    int calculatedBackwardPageNumber => CurrentPage - XwardStepSize;
    int calculatedForwardPageNumber => CurrentPage + XwardStepSize;
    int remainingPageCount => TotalPages - CurrentPage;

    public int PreviousPageNumber => CurrentPage - 1;
    public int NextPageNumber => CurrentPage + 1;
    public int PagerStartIndex => calculatedStartIndex <= DefaultStartIndex ? DefaultStartIndex : calculatedStartIndex;
    public int PagerEndIndex => calculatedEndIndex >= TotalPages ? TotalPages : calculatedEndIndex;
    public int BackwardPageNumber => (PreviousPageNumber >= XwardStepSize) ? calculatedBackwardPageNumber : 0;
    public int ForwardPageNumber => (remainingPageCount >= XwardStepSize) ? calculatedForwardPageNumber : 0;

    public bool ShowPrevious => CurrentPage > DefaultStartIndex;
    public bool ShowNext => CurrentPage < TotalPages;
    public bool ShowFirst => CurrentPage != DefaultStartIndex;
    public bool ShowLast => CurrentPage != TotalPages;
    public bool ShowBackward => BackwardPageNumber != 0;
    public bool ShowForward => ForwardPageNumber != 0;

}
