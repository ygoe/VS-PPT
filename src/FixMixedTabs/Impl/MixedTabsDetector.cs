using Microsoft.VisualStudio.Text;

namespace FixMixedTabs
{
    public static class MixedTabsDetector
    {
        public static bool HasMixedTabsAndSpaces(int tabSize, ITextSnapshot snapshot)
        {
            int lineNo;
            return HasMixedTabsAndSpaces(tabSize, snapshot, out lineNo);
        }

        public static bool HasMixedTabsAndSpaces(int tabSize, ITextSnapshot snapshot, out int lineNo)
        {
            bool seenSpaces = false;
            bool seenTabs = false;

            foreach (var line in snapshot.Lines)
            {
                if (line.Length > 0)
                {
                    for (int i = line.Start.Position; i < line.End.Position; i++)
                    {
                        char ch = snapshot[i];
                        if (ch == ' ')
                        {
                            seenSpaces = true;
                        }
                        else if (ch == '\t')
                        {
                            seenTabs = true;
                        }
                        else if (char.IsWhiteSpace(ch))
                        {
                            lineNo = line.LineNumber;
                            return true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (seenSpaces && seenTabs)
                    {
                        lineNo = line.LineNumber;
                        return true;
                    }
                }
            }

            lineNo = -1;
            return false;
        }
    }
}
