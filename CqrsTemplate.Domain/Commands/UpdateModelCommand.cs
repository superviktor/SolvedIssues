using CqrsTemplate.Domain.Common;
using System;

namespace CqrsTemplate.Domain.Commands
{
    public class UpdateModelCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
