namespace DocPortal.Application.Options
{
  public class PageOptions
  {
    private int _pageSize = 25;
    private int _pageToken = 1;

    public PageOptions(int? pageSize, int? pageToken)
    {
      this.PageSize = pageSize ?? _pageSize;
      this.PageToken = pageToken ?? _pageToken;
    }


    public int PageSize
    {
      get => _pageSize; set
      {
        if (value is > 0 and < 25)
        {
          _pageSize = value;
        }
      }
    }
    public int PageToken
    {
      get => _pageToken; set
      {
        if (value > 0)
        {
          _pageToken = value;
        }
      }
    }
  }
}
