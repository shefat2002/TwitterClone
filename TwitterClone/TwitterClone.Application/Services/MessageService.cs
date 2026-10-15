using TwitterClone.Application.Dtos.Message;
using TwitterClone.Application.Interfaces;
using TwitterClone.Domain.Entities;

namespace TwitterClone.Application.Services;

public class MessageService:IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;

    public MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }

    public MessageDto? SendMessage(CreateMessageDto createMessageDto)
    {
        if(string.IsNullOrWhiteSpace(createMessageDto.Content))
        {
            return null;
        }
        if(createMessageDto.SenderId == createMessageDto.ReceiverId)
        {
            return null;
        }
        if(_userRepository.GetUserById(createMessageDto.SenderId) == null || _userRepository.GetUserById(createMessageDto.ReceiverId) == null)
        {
            return null;
        }
        var sendMessage = _messageRepository.AddMessage(new Message()
        {
            SenderId = createMessageDto.SenderId,
            ReceiverId = createMessageDto.ReceiverId,
            Content = createMessageDto.Content.Trim(),
            SentAt = DateTime.UtcNow,
            IsRead = false
        });
        return new MessageDto
        {
            Id = sendMessage.Id,
            SenderId = sendMessage.SenderId,
            ReceiverId = sendMessage.ReceiverId,
            Content = sendMessage.Content,
            SentAt = sendMessage.SentAt,
            IsRead = sendMessage.IsRead
        };
    }

    public MessageDto GetMessageById(Guid id)
    {
        var message = _messageRepository.GetMessageById(id);
        if(message == null)
        {
            return null;
        }
        return new MessageDto
        {
            Id = message.Id,
            SenderId = message.SenderId,
            ReceiverId = message.ReceiverId,
            Content = message.Content,
            SentAt = message.SentAt,
            IsRead = message.IsRead
        };
    }

    public List<MessageDto> GetMessages(Guid? senderId, Guid? receiverId)
    {
        return _messageRepository.GetMessages(senderId, receiverId)
            .OrderByDescending(message => message.SentAt)
            .Select(message => new MessageDto
            {
                Id = message.Id,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                Content = message.Content,
                SentAt = message.SentAt,
                IsRead = message.IsRead
            }).ToList();
    }

    public List<MessageDto> GetConversation(Guid userId, Guid otherUserId)
    {
        return _messageRepository.GetConversation(userId, otherUserId)
            .OrderByDescending(message => message.SentAt)
            .Select(message => new MessageDto
            {
                Id = message.Id,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                Content = message.Content,
                SentAt = message.SentAt,
                IsRead = message.IsRead
            }).ToList();
    }

    public MessageDto? UpdateMessage(Guid id, UpdateMessageDto updateMessageDto)
    {
        if(!string.IsNullOrWhiteSpace(updateMessageDto.Content))
        {
            return null;
        }
        var message = _messageRepository.GetMessageById(id);
        if (message == null)
        {
            return null;
        }
        message.Content = updateMessageDto.Content.Trim();
        message.MarkAsModified();
        _messageRepository.UpdateMessage(message);
        return new MessageDto
        {
            Id = message.Id,
            SenderId = message.SenderId,
            ReceiverId = message.ReceiverId,
            Content = message.Content,
            SentAt = message.SentAt,
            IsRead = message.IsRead
        };
    }

    public MessageDto? MarkMessageAsRead(Guid id)
    {
        var message = _messageRepository.GetMessageById(id);
        if (message == null)
        {
            return null;
        }
        if(!message.IsRead)
        {
            message.IsRead = true;
            message.MarkAsModified();
            _messageRepository.UpdateMessage(message);
        }
        return new MessageDto
        {
            Id = message.Id,
            SenderId = message.SenderId,
            ReceiverId = message.ReceiverId,
            Content = message.Content,
            SentAt = message.SentAt,
            IsRead = message.IsRead
        };
    }

    public bool DeleteMessage(Guid id)
    {
        var message = _messageRepository.GetMessageById(id);
        if (message == null)
        {
            return false;
        }
        return _messageRepository.DeleteMessage(message);
    }
}