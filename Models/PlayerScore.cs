using System;
using System.Collections.Generic;

namespace ECF.Models;

public partial class PlayerScore
{
    public int ScoreId { get; set; }

    public string PlayerName { get; set; } = null!;

    public int Score { get; set; }

    public DateTime? DateRecorded { get; set; }
}
