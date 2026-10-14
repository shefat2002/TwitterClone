using TwitterClone.Application.Dtos.Bookmark;

namespace TwitterClone.Application.Interfaces;

public interface IBookmarkService
{
    BookmarkDto? CreateBookmark(CreateBookmarkDto createBookmarkDto);
    BookmarkDto? GetBookmarkById(Guid id);
    List<BookmarkDto> GetBookmarks(Guid? userId);
    bool DeleteBookmark(Guid id);
}