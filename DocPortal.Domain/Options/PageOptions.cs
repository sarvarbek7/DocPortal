namespace DocPortal.Domain.Options
{
  public class PageOptions
  {
    private int _pageSize = 20;
    private int _pageToken = 1;

    public int PageSize
    {
      get => _pageSize; set
      {
        if (value is > 0 and < 20)
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
