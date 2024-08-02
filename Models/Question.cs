using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ECF.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public string Text { get; set; } = null!;

    public string CorrectAnswer { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();
}
