using GigaChat.Core.Common.Entities.ChatMessages;
using GigaChat.Core.Common.Entities.ChatRooms;
using GigaChat.Core.Common.Entities.Users;
using GigaChat.Data.Configurations.Base;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigaChat.Data.Configurations;

public class ChatMessageConfiguration : EntityConfigurationBase<ChatMessage, long>
{
    public override void OnConfigure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.Property(m => m.Text)
            .IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(m => m.UserId)
            .IsRequired();

        builder.HasOne<ChatRoom>()
            .WithMany()
            .HasForeignKey(m => m.ChatRoomId)
            .IsRequired();
    }
}