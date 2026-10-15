using TwitterClone.Application.Dtos.Bookmark;
using TwitterClone.Application.Interfaces;
using TwitterClone.Domain.Entities;

namespace TwitterClone.Application.Services;

public class BookmarkService : IBookmarkService
{
    private readonly IBookmarkRepository _bookmarkRepository;
    private readonly ITweetRepository _tweetRepository;
    private readonly IUserRepository _userRepository;

    public BookmarkService(IBookmarkRepository bookmarkRepository, ITweetRepository tweetRepository, IUserRepository userRepository)
    {
        _bookmarkRepository = bookmarkRepository;
        _tweetRepository = tweetRepository;
        _userRepository = userRepository;
    }

    public BookmarkDto? CreateBookmark(CreateBookmarkDto createBookmarkDto)
    {
        if(_userRepository.GetUserById(createBookmarkDto.UserId) == null ||
           _tweetRepository.GetTweetById(createBookmarkDto.TweetId) == null)
        {
            return null;
        }
        var existingBookmark = _bookmarkRepository.GetBookmarkByUserAndTweet(
            createBookmarkDto.UserId, createBookmarkDto.TweetId);
        if(existingBookmark != null)
        {
            return new BookmarkDto
            {
                Id = existingBookmark.Id,
                UserId = existingBookmark.UserId,
                TweetId = existingBookmark.TweetId
            };
        }
        var createdBookmark = _bookmarkRepository.AddBookmark(new Bookmark
        {
            UserId = createBookmarkDto.UserId,
            TweetId = createBookmarkDto.TweetId
        });
        return new BookmarkDto
        {
            Id = createdBookmark.Id,
            UserId = createdBookmark.UserId,
            TweetId = createdBookmark.TweetId
        };
    }

    public BookmarkDto? GetBookmarkById(Guid id)
    {
        var bookmark = _bookmarkRepository.GetBookmarkById(id);
        if(bookmark == null)
        {
            return null;
        }
        return new BookmarkDto
        {
            Id = bookmark.Id,
            UserId = bookmark.UserId,
            TweetId = bookmark.TweetId
        };
    }

    public List<BookmarkDto> GetBookmarks(Guid? userId)
    {
        var bookmarks = _bookmarkRepository.GetBookmarks(userId);
        return bookmarks.Select(b => new BookmarkDto
        {
            Id = b.Id,
            UserId = b.UserId,
            TweetId = b.TweetId
        }).ToList();
    }

    public bool DeleteBookmark(Guid id)
    {
        var bookmark = _bookmarkRepository.GetBookmarkById(id);
        if(bookmark == null)
        {
            return false;
        }
        return _bookmarkRepository.DeleteBookmark(bookmark);
    }
}