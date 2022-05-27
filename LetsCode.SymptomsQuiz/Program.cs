var results = new Dictionary<string, string> {
  { "SSNNS", "Dengue" },
  { "SSSSN", "Gripe" },
  { "NSSSN", "Gripe" },
  { "SNNNN", "Sem doenças" },
  { "NNNNN", "Sem doenças" },
};

char GetConfirm(string message) {
  Console.WriteLine(message);
  Console.Write("[s]im ou [n]ão (padrão: não): ");

  var key = Console.ReadKey().Key;

  var answer = key switch {
    ConsoleKey.Y or ConsoleKey.S => 'S',
    ConsoleKey.N => 'N',
    _ => 'N'
  };

  Console.WriteLine();

  return answer;
}

var questions = new[] {
  GetConfirm("Sente dor no corpo?"),
  GetConfirm("Você tem febre?"),
  GetConfirm("Você tem tosse?"),
  GetConfirm("Está com congestão nasal?"),
  GetConfirm("Tem manchas pelo corpo?")
};

Console.WriteLine($"Você tem: {results[string.Join("", questions)]}");
