/*
 * MarkdownSharp
 * -------------
 * a C# Markdown processor
 *
 * Markdown is a text-to-HTML conversion tool for web writers
 * Copyright (c) 2004 John Gruber
 * http://daringfireball.net/projects/markdown/
 *
 * Markdown.NET
 * Copyright (c) 2004-2009 Milan Negovan
 * http://www.aspnetresources.com
 * http://aspnetresources.com/blog/markdown_announced.aspx
 *
 * MarkdownSharp
 * Copyright (c) 2009-2011 Jeff Atwood
 * http://stackoverflow.com
 * http://www.codinghorror.com/blog/
 * http://code.google.com/p/markdownsharp/
 *
 * History: Milan ported the Markdown processor to C#. He granted license to me so I can open source it
 * and let the community contribute to and improve MarkdownSharp.
 *
 * No copyright 2013 Sascha Kiefer (@esskar)
 * Made some changes (specially cleanup, article specification and retrieving, as well as image retrieving)
 */

#region Copyright and license

/*

Copyright (c) 2009 - 2010 Jeff Atwood

http://www.opensource.org/licenses/mit-license.php

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

Copyright (c) 2003-2004 John Gruber
<http://daringfireball.net/>
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:

* Redistributions of source code must retain the above copyright notice,
  this list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright
  notice, this list of conditions and the following disclaimer in the
  documentation and/or other materials provided with the distribution.

* Neither the name "Markdown" nor the names of its contributors may
  be used to endorse or promote products derived from this software
  without specific prior written permission.

This software is provided by the copyright holders and contributors "as
is" and any express or implied warranties, including, but not limited
to, the implied warranties of merchantability and fitness for a
particular purpose are disclaimed. In no event shall the copyright owner
or contributors be liable for any direct, indirect, incidental, special,
exemplary, or consequential damages (including, but not limited to,
procurement of substitute goods or services; loss of use, data, or
profits; or business interruption) however caused and on any theory of
liability, whether in contract, strict liability, or tort (including
negligence or otherwise) arising in any way out of the use of this
software, even if advised of the possibility of such damage.
*/

#endregion

#pragma warning disable SA1204 // Disable Static Methods before non-static's
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace AP.MobileToolkit.Markdown
{
    /// <summary>
    /// Markdown is a text-to-HTML conversion tool for web writers.
    /// Markdown allows you to write using an easy-to-read, easy-to-write plain text format,
    /// then convert it to structurally valid XHTML (or HTML).
    /// </summary>
    internal class Markdown : MarkdownOptions
    {
#pragma warning disable SA1623
        /// <summary>
        /// current version of MarkdownSharp;
        /// see https://github.com/esskar/MarkdownSharp for the latest code or to contribute
        /// </summary>
        public string Version => "1.15";
#pragma warning restore SA1623

        #region Constructors and Options

        /// <summary>
        /// Create a new Markdown instance and optionally load options from a configuration
        /// file. There they should be stored in the appSettings section, available options are:
        ///
        ///     Markdown.StrictBoldItalic (true/false)
        ///     Markdown.EmptyElementSuffix (">" or " />" without the quotes)
        ///     Markdown.LinkEmails (true/false)
        ///     Markdown.AutoNewLines (true/false)
        ///     Markdown.AutoHyperlink (true/false)
        ///     Markdown.EncodeProblemUrlCharacters (true/false)
        ///
        /// </summary>
        public Markdown(bool loadOptionsFromConfigFile = false)
        {
            EmptyElementSuffix = " />";
            LinkEmails = true;

            if (!loadOptionsFromConfigFile)
                return;

            // var settings = ConfigurationManager.AppSettings;
            // foreach (string key in settings.Keys)
            // {
            //        switch (key)
            //        {
            //            case "Markdown.AutoHyperlink":
            //                this.AutoHyperlink = Convert.ToBoolean(settings[key]);
            //                break;
            //            case "Markdown.AutoNewlines":
            //                this.AutoNewLines = Convert.ToBoolean(settings[key]);
            //                break;
            //            case "Markdown.EmptyElementSuffix":
            //                this.EmptyElementSuffix = settings[key];
            //                break;
            //            case "Markdown.EncodeProblemUrlCharacters":
            //                this.EncodeProblemUrlCharacters = Convert.ToBoolean(settings[key]);
            //                break;
            //            case "Markdown.LinkEmails":
            //                this.LinkEmails = Convert.ToBoolean(settings[key]);
            //                break;
            //            case "Markdown.StrictBoldItalic":
            //                this.StrictBoldItalic = Convert.ToBoolean(settings[key]);
            //                break;
            //            case "Markdown.SupportArticles":
            //                this.SupportArticles = Convert.ToBoolean(settings[key]);
            //                break;
            //        }
            // }
        }

        /// <summary>
        /// Create a new Markdown instance and set the options from the MarkdownOptions object.
        /// </summary>
        public Markdown(MarkdownOptions options)
        {
            AutoHyperlink = options.AutoHyperlink;
            AutoNewLines = options.AutoNewLines;
            EmptyElementSuffix = options.EmptyElementSuffix;
            EncodeProblemUrlCharacters = options.EncodeProblemUrlCharacters;
            LinkEmails = options.LinkEmails;
            StrictBoldItalic = options.StrictBoldItalic;
            SupportArticles = options.SupportArticles;
        }
        #endregion

        private enum TokenType
        {
            Text,
            Tag
        }

        private struct Token
        {
            public Token(TokenType type, string value)
            {
                Type = type;
                Value = value;
            }

            public TokenType Type { get; }

            public string Value { get; }
        }

        /// <summary>
        /// maximum nested depth of [] and () supported by the transform; implementation detail
        /// </summary>
        private const int NestDepth = 6;

        /// <summary>
        /// Tabs are automatically converted to spaces as part of the transform
        /// this constant determines how "wide" those tabs become in spaces
        /// </summary>
        private const int TabWidth = 4;

        private const string MarkerUl = @"[*+-]";
        private const string MarkerOl = @"\d+[.]";

        // temporarily replaces "://" where auto-linking shouldn't happen;
        private const string AutoLinkPreventionMarker = "\x1AP";

        private static readonly Dictionary<string, string> _escapeTable;
        private static readonly Dictionary<string, string> _invertedEscapeTable;
        private static readonly Dictionary<string, string> _backslashEscapeTable;

        private readonly Dictionary<string, string> _urls = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _titles = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _htmlBlocks = new Dictionary<string, string>();

        private readonly List<string> _articles = new List<string>();
        private readonly Dictionary<string, string> _images = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        private int _listLevel;

        /// <summary>
        /// In the static constuctor we'll initialize what stays the same across all transforms.
        /// </summary>
        static Markdown()
        {
            // Table of hash values for escaped characters:
            _escapeTable = new Dictionary<string, string>();
            _invertedEscapeTable = new Dictionary<string, string>();

            // Table of hash value for backslash escaped characters:
            _backslashEscapeTable = new Dictionary<string, string>();

            var backslashPattern = string.Empty;

            foreach (var c in @"\`*_{}[]()>#+-.!")
            {
                var key = c.ToString(CultureInfo.InvariantCulture);
                var hash = GetHashKey(key, false);
                _escapeTable.Add(key, hash);
                _invertedEscapeTable.Add(hash, key);
                _backslashEscapeTable.Add(@"\" + key, hash);
                backslashPattern += Regex.Escape(@"\" + key) + "|";
            }

            _backslashEscapes = new Regex(backslashPattern.Substring(0, backslashPattern.Length - 1), RegexOptions.Compiled);
        }

        /// <summary>
        /// Gets the article count.
        /// </summary>
        /// <value>
        /// The article count.
        /// </value>
        public int ArticleCount => _articles.Count;

        /// <summary>
        /// Gets the article.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="transformed">if set to <c>true</c> markdown inside the article will be replaced, if <c>false</c> otherwise.</param>
        /// <returns></returns>
        /// <exception cref="System.IndexOutOfRangeException"></exception>
        public string GetArticle(int index, bool transformed = true)
        {
            if (index < 0 || index >= ArticleCount)
                throw new IndexOutOfRangeException();

            if (!transformed)
                return _articles[index];

            var markdown = new Markdown(this);
            return markdown.Transform(_articles[index]);
        }

        /// <summary>
        /// Gets the articles.
        /// </summary>
        /// <param name="transformed">if set to <c>true</c> markdown inside the article will be replaced, if <c>false</c> otherwise.</param>
        /// <returns></returns>
        public string[] GetArticles(bool transformed = true)
        {
            if (!transformed)
                return _articles.ToArray();

            var markdown = new Markdown(this);

            var retval = new string[_articles.Count];
            for (var i = 0; i < _articles.Count; ++i)
                retval[i] = markdown.Transform(_articles[i]);
            return retval;
        }

        /// <summary>
        /// Gets the image count.
        /// </summary>
        /// <value>
        /// The image count.
        /// </value>
        public int ImageCount => _images.Count;

        /// <summary>
        /// Gets the image by alt-text
        /// </summary>
        /// <param name="altText">The alt text.</param>
        /// <returns></returns>
        public string GetImage(string altText)
        {
            _images.TryGetValue(altText, out string image);
            return image;
        }

        /// <summary>
        /// Transforms the provided Markdown-formatted text to HTML;
        /// see http://en.wikipedia.org/wiki/Markdown
        /// </summary>
        /// <remarks>
        /// The order in which other subs are called here is
        /// essential. Link and image substitutions need to happen before
        /// EscapeSpecialChars(), so that any *'s or _'s in the a
        /// and img tags get encoded.
        /// </remarks>
        public string Transform(string text)
        {
            Setup();

            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            text = Normalize(text);

            text = HashHtmlBlocks(text);
            text = StripLinkDefinitions(text);
            text = RunBlockGamut(text);
            text = Unescape(text);

            return text + "\n";
        }

        /// <summary>
        /// Perform transformations that form block-level tags like paragraphs, headers, and list items.
        /// </summary>
        private string RunBlockGamut(string text, bool unhash = true)
        {
            text = DoHeaders(text);
            text = DoHorizontalRules(text);
            text = DoLists(text);
            text = DoCodeBlocks(text);
            text = DoArticles(text);
            text = DoBlockQuotes(text);

            // We already ran HashHTMLBlocks() before, in Markdown(), but that
            // was to escape raw HTML in the original Markdown source. This time,
            // we're escaping the markup we've just created, so that we don't wrap
            // <p> tags around block-level tags.
            text = HashHtmlBlocks(text);

            text = FormParagraphs(text, unhash);

            return text;
        }

        /// <summary>
        /// Perform transformations that occur *within* block-level tags like paragraphs, headers, and list items.
        /// </summary>
        private string RunSpanGamut(string text)
        {
            text = DoCodeSpans(text);
            text = EscapeSpecialCharsWithinTagAttributes(text);
            text = EscapeBackslashes(text);

            // Images must come first, because ![foo][f] looks like an anchor.
            text = DoImages(text);
            text = DoAnchors(text);

            // Must come after DoAnchors(), because you can use < and >
            // delimiters in inline links like [this](<url>).
            text = DoAutoLinks(text);

            text = text.Replace(AutoLinkPreventionMarker, "://");

            text = EncodeAmpsAndAngles(text);
            text = DoItalicsAndBold(text);
            text = DoHardBreaks(text);

            return text;
        }

        private static readonly Regex _newlinesLeadingTrailing = new Regex(@"^\n+|\n+\z", RegexOptions.Compiled);
        private static readonly Regex _newlinesMultiple = new Regex(@"\n{2,}", RegexOptions.Compiled);
        private static readonly Regex _leadingWhitespace = new Regex(@"^[ ]*", RegexOptions.Compiled);

        private static readonly Regex _htmlBlockHash = new Regex("\x1AH\\d+H", RegexOptions.Compiled);

        /// <summary>
        /// splits on two or more newlines, to form "paragraphs";
        /// each paragraph is then unhashed (if it is a hash and unhashing isn't turned off) or wrapped in HTML p tag
        /// </summary>
        private string FormParagraphs(string text, bool unhash = true)
        {
            // split on two or more newlines
            var grafs = _newlinesMultiple.Split(_newlinesLeadingTrailing.Replace(text, string.Empty));

            for (var i = 0; i < grafs.Length; i++)
            {
                if (grafs[i].StartsWith("\x1AH"))
                {
                    // unhashify HTML blocks
                    if (unhash)
                    {
                        var sanityCheck = 50; // just for safety, guard against an infinite loop
                        var keepGoing = true; // as long as replacements where made, keep going
                        while (keepGoing && sanityCheck > 0)
                        {
                            keepGoing = false;
                            grafs[i] = _htmlBlockHash.Replace(grafs[i], match =>
                            {
                                keepGoing = true;
                                return _htmlBlocks[match.Value];
                            });
                            sanityCheck--;
                        }
                        /* if (keepGoing)
                        {
                            // Logging of an infinite loop goes here.
                            // If such a thing should happen, please open a new issue on http://code.google.com/p/markdownsharp/
                            // with the input that caused it.
                        }*/
                    }
                }
                else
                {
                    // do span level processing inside the block, then wrap result in <p> tags
                    grafs[i] = _leadingWhitespace.Replace(RunSpanGamut(grafs[i]), "<p>") + "</p>";
                }
            }

            return string.Join("\n\n", grafs);
        }

        private void Setup()
        {
            // Clear the global hashes. If we don't clear these, you get conflicts
            // from other articles when generating a page which contains more than
            // one article (e.g. an index page that shows the N most recent
            // articles):
            _articles.Clear();
            _images.Clear();
            _urls.Clear();
            _titles.Clear();
            _htmlBlocks.Clear();
            _listLevel = 0;
        }

        private static string _nestedBracketsPattern;

        /// <summary>
        /// Reusable pattern to match balanced [brackets]. See Friedl's
        /// "Mastering Regular Expressions", 2nd Ed., pp. 328-331.
        /// </summary>
        private static string GetNestedBracketsPattern()
        {
            // in other words [this] and [this[also]] and [this[also[too]]]
            // up to _nestDepth
            if (_nestedBracketsPattern != null)
                return _nestedBracketsPattern;

            _nestedBracketsPattern =
                    RepeatString(
                        @"
                    (?>              # Atomic matching
                       [^\[\]]+      # Anything other than brackets
                     |
                       \[
                           ", NestDepth) + RepeatString(
                    @" \]
                    )*",
                    NestDepth);

            return _nestedBracketsPattern;
        }

        private static string _nestedParensPattern;

        /// <summary>
        /// Reusable pattern to match balanced (parens). See Friedl's
        /// "Mastering Regular Expressions", 2nd Ed., pp. 328-331.
        /// </summary>
        private static string GetNestedParensPattern()
        {
            // in other words (this) and (this(also)) and (this(also(too)))
            // up to _nestDepth
            if (_nestedParensPattern != null)
                return _nestedParensPattern;

            _nestedParensPattern =
                    RepeatString(
                        @"
                    (?>              # Atomic matching
                       [^()\s]+      # Anything other than parens or whitespace
                     |
                       \(
                           ", NestDepth) + RepeatString(
                    @" \)
                    )*",
                    NestDepth);

            return _nestedParensPattern;
        }

        private static readonly Regex _linkDef = new Regex(
            string.Format(
            @"
                        ^[ ]{{0,{0}}}\[(.+)\]:  # id = $1
                          [ ]*
                          \n?                   # maybe *one* newline
                          [ ]*
                        <?(\S+?)>?              # url = $2
                          [ ]*
                          \n?                   # maybe one newline
                          [ ]*
                        (?:
                            (?<=\s)             # lookbehind for whitespace
                            [""(]
                            (.+?)               # title = $3
                            ["")]
                            [ ]*
                        )?                      # title is optional
                        (?:\n+|\Z)", TabWidth - 1),
            RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// Strips link definitions from text, stores the URLs and titles in hash references.
        /// </summary>
        /// <remarks>
        /// ^[id]: url "optional title"
        /// </remarks>
        private string StripLinkDefinitions(string text)
        {
            return _linkDef.Replace(text, LinkEvaluator);
        }

        private string LinkEvaluator(Match match)
        {
            var linkId = match.Groups[1].Value.ToLowerInvariant();
            _urls[linkId] = EncodeAmpsAndAngles(match.Groups[2].Value);

            if (match.Groups[3].Length > 0)
                _titles[linkId] = match.Groups[3].Value.Replace("\"", "&quot;");

            return string.Empty;
        }

        // compiling this monster regex results in worse performance. trust me.
        private static readonly Regex _blocksHtml = new Regex(GetBlockPattern(), RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);

        /// <summary>
        /// derived pretty much verbatim from PHP Markdown
        /// </summary>
        private static string GetBlockPattern()
        {
            // Hashify HTML blocks:
            // We only want to do this for block-level HTML tags, such as headers,
            // lists, and tables. That's because we still want to wrap <p>s around
            // "paragraphs" that are wrapped in non-block-level tags, such as anchors,
            // phrase emphasis, and spans. The list of tags we're looking for is
            // hard-coded:
            //
            // *  List "a" is made of tags which can be both inline or block-level.
            //    These will be treated block-level when the start tag is alone on
            //    its line, otherwise they're not matched here and will be taken as
            //    inline later.
            // *  List "b" is made of tags which are always block-level;
            const string blockTagsA = "ins|del";
            const string blockTagsB = "p|div|h[1-6]|blockquote|pre|table|dl|ol|ul|address|script|noscript|form|fieldset|iframe|math";

            // Regular expression for the content of a block tag.
            const string attr = @"
            (?>				            # optional tag attributes
              \s			            # starts with whitespace
              (?>
                [^>""/]+	            # text outside quotes
              |
                /+(?!>)		            # slash not followed by >
              |
                ""[^""]*""		        # text inside double quotes (tolerate >)
              |
                '[^']*'	                # text inside single quotes (tolerate >)
              )*
            )?	
            ";

#pragma warning disable SA1025
            var content = RepeatString(
                @"
                (?>
                  [^<]+			        # content without tag
                |
                  <\2			        # nested opening tag
                    " + attr + @"       # attributes
                  (?>
                      />
                  |
                      >", NestDepth) +   // end of opening tag
                      ".*?" +             // last level nested tag content
            RepeatString(
                @"
                      </\2\s*>	        # closing nested tag
                  )
                  |				
                  <(?!/\2\s*>           # other tags with a different name
                  )
                )*",
                NestDepth);
#pragma warning restore SA1025

            var content2 = content.Replace(@"\2", @"\3");

            // First, look for nested blocks, e.g.:
            // <div>
            //   <div>
            //   tags for inner block must be indented.
            //   </div>
            // </div>
            //
            // The outermost tags must start at the left margin for this to match, and
            // the inner nested divs must be indented.
            // We need to do this before the next, more liberal match, because the next
            // match will start at the first `<div>` and stop at the first `</div>`.
            var pattern = @"
            (?>
                  (?>
                    (?<=\n)     # Starting at the beginning of a line
                    |           # or
                    \A\n?       # the beginning of the doc
                  )
                  (             # save in $1

                    # Match from `\n<tag>` to `</tag>\n`, handling nested tags 
                    # in between.
                      
                        <($block_tags_b_re)   # start tag = $2
                        $attr>                # attributes followed by > and \n
                        $content              # content, support nesting
                        </\2>                 # the matching end tag
                        [ ]*                  # trailing spaces
                        (?=\n+|\Z)            # followed by a newline or end of document

                  | # Special version for tags of group a.

                        <($block_tags_a_re)   # start tag = $3
                        $attr>[ ]*\n          # attributes followed by >
                        $content2             # content, support nesting
                        </\3>                 # the matching end tag
                        [ ]*                  # trailing spaces
                        (?=\n+|\Z)            # followed by a newline or end of document
                      
                  | # Special case just for <hr />. It was easier to make a special 
                    # case than to make the other regex more complicated.
                  
                        [ ]{0,$less_than_tab}
                        <hr
                        $attr                 # attributes
                        /?>                   # the matching end tag
                        [ ]*
                        (?=\n{2,}|\Z)         # followed by a blank line or end of document
                  
                  | # Special case for standalone HTML comments:
                  
                      (?<=\n\n|\A)            # preceded by a blank line or start of document
                      [ ]{0,$less_than_tab}
                      (?s:
                        <!--(?:|(?:[^>-]|-[^>])(?:[^-]|-[^-])*)-->
                      )
                      [ ]*
                      (?=\n{2,}|\Z)            # followed by a blank line or end of document
                  
                  | # PHP and ASP-style processor instructions (<? and <%)
                  
                      [ ]{0,$less_than_tab}
                      (?s:
                        <([?%])                # $4
                        .*?
                        \4>
                      )
                      [ ]*
                      (?=\n{2,}|\Z)            # followed by a blank line or end of document
                      
                  )
            )";

            pattern = pattern.Replace("$less_than_tab", (TabWidth - 1).ToString(CultureInfo.InvariantCulture));
            pattern = pattern.Replace("$block_tags_b_re", blockTagsB);
            pattern = pattern.Replace("$block_tags_a_re", blockTagsA);
            pattern = pattern.Replace("$attr", attr);
            pattern = pattern.Replace("$content2", content2);
            pattern = pattern.Replace("$content", content);

            return pattern;
        }

        /// <summary>
        /// replaces any block-level HTML blocks with hash entries
        /// </summary>
        private string HashHtmlBlocks(string text)
        {
            return _blocksHtml.Replace(text, HtmlEvaluator);
        }

        private string HtmlEvaluator(Match match)
        {
            var text = match.Groups[1].Value;
            var key = GetHashKey(text, true);
            _htmlBlocks[key] = text;

            return string.Concat("\n\n", key, "\n\n");
        }

        private static string GetHashKey(string s, bool isHtmlBlock)
        {
            var delim = isHtmlBlock ? 'H' : 'E';
            return string.Format("\x1A{0}{1}{0}", delim, Math.Abs(s.GetHashCode()));
        }

        private static readonly Regex _htmlTokens = new Regex(
            @"
            (<!--(?:|(?:[^>-]|-[^>])(?:[^-]|-[^-])*)-->)|        # match <!-- foo -->
            (<\?.*?\?>)|                 # match <?foo?> " +
            RepeatString(
                @" 
            (<[A-Za-z\/!$](?:[^<>]|", NestDepth) + RepeatString(@")*>)", NestDepth) +
                                       " # match <tag> and </tag>",
            RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// returns an array of HTML tokens comprising the input string. Each token is
        /// either a tag (possibly with nested, tags contained therein, such
        /// as &lt;a href="&lt;MTFoo&gt;"&gt;, or a run of text between tags. Each element of the
        /// array is a two-element array; the first is either 'tag' or 'text'; the second is
        /// the actual value.
        /// </summary>
        private static IEnumerable<Token> TokenizeHtml(string text)
        {
            var pos = 0;
            var tokens = new List<Token>();

            // this regex is derived from the _tokenize() subroutine in Brad Choate's MTRegex plugin.
            // http://www.bradchoate.com/past/mtregex.php
            foreach (Match m in _htmlTokens.Matches(text))
            {
                var tagStart = m.Index;

                if (pos < tagStart)
                    tokens.Add(new Token(TokenType.Text, text.Substring(pos, tagStart - pos)));

                tokens.Add(new Token(TokenType.Tag, m.Value));
                pos = tagStart + m.Length;
            }

            if (pos < text.Length)
                tokens.Add(new Token(TokenType.Text, text.Substring(pos, text.Length - pos)));

            return tokens;
        }

        private static readonly Regex _anchorRef =
            new Regex(
                string.Format(
                    @"
            (                               # wrap whole match in $1
                \[
                    ({0})                   # link text = $2
                \]

                [ ]?                        # one optional space
                (?:\n[ ]*)?                 # one optional newline followed by spaces

                \[
                    (.*?)                   # id = $3
                \]
            )",
                    GetNestedBracketsPattern()), RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private static readonly Regex _anchorInline =
            new Regex(
                string.Format(
            @"
                (                           # wrap whole match in $1
                    \[
                        ({0})               # link text = $2
                    \]
                    \(                      # literal paren
                        [ ]*
                        ({1})               # href = $3
                        [ ]*
                        (                   # $4
                        (['""])           # quote char = $5
                        (.*?)               # title = $6
                        \5                  # matching quote
                        [ ]*                # ignore any spaces between closing quote and )
                        )?                  # title is optional
                    \)
                )",
            GetNestedBracketsPattern(),
            GetNestedParensPattern()),
                RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private static readonly Regex _anchorRefShortcut = new Regex(
            @"
            (                               # wrap whole match in $1
              \[
                 ([^\[\]]+)                 # link text = $2; can't contain [ or ]
              \]
            )", RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// Turn Markdown link shortcuts into HTML anchor tags
        /// </summary>
        /// <remarks>
        /// [link text](url "title")
        /// [link text][id]
        /// [id]
        /// </remarks>
        private string DoAnchors(string text)
        {
            // First, handle reference-style links: [link text] [id]
            text = _anchorRef.Replace(text, AnchorRefEvaluator);

            // Next, inline-style links: [link text](url "optional title") or [link text](url "optional title")
            text = _anchorInline.Replace(text, AnchorInlineEvaluator);

            // Last, handle reference-style shortcuts: [link text]
            // These must come last in case you've also got [link test][1]
            // or [link test](/foo)
            return _anchorRefShortcut.Replace(text, AnchorRefShortcutEvaluator);
        }

        private static string SaveFromAutoLinking(string s)
        {
            return s.Replace("://", AutoLinkPreventionMarker);
        }

        private string AnchorRefEvaluator(Match match)
        {
            var wholeMatch = match.Groups[1].Value;
            var linkText = SaveFromAutoLinking(match.Groups[2].Value);
            var linkId = match.Groups[3].Value.ToLowerInvariant();

            // for shortcut links like [this][].
            if (linkId == string.Empty)
                linkId = linkText.ToLowerInvariant();

            if (!_urls.ContainsKey(linkId))
                return wholeMatch;

            var url = _urls[linkId];

            url = EncodeProblemUrlChars(url);
            url = EscapeBoldItalic(url);

            var result = "<a href=\"" + url + "\"";
            if (_titles.ContainsKey(linkId))
            {
                var title = AttributeEncode(_titles[linkId]);
                title = AttributeEncode(EscapeBoldItalic(title));
                result += " title=\"" + title + "\"";
            }
            result += ">" + linkText + "</a>";

            return result;
        }

        private string AnchorRefShortcutEvaluator(Match match)
        {
            var wholeMatch = match.Groups[1].Value;
            var linkText = SaveFromAutoLinking(match.Groups[2].Value);
            var linkId = Regex.Replace(linkText.ToLowerInvariant(), @"[ ]*\n[ ]*", " ");  // lower case and remove newlines / extra spaces

            if (!_urls.ContainsKey(linkId))
                return wholeMatch;

            var url = _urls[linkId];

            url = EncodeProblemUrlChars(url);
            url = EscapeBoldItalic(url);

            var result = "<a href=\"" + url + "\"";
            if (_titles.ContainsKey(linkId))
            {
                var title = AttributeEncode(_titles[linkId]);
                title = EscapeBoldItalic(title);
                result += " title=\"" + title + "\"";
            }

            result += ">" + linkText + "</a>";

            return result;
        }

        private string AnchorInlineEvaluator(Match match)
        {
            var linkText = SaveFromAutoLinking(match.Groups[2].Value);
            var url = match.Groups[3].Value;
            var title = match.Groups[6].Value;

            url = EncodeProblemUrlChars(url);
            url = EscapeBoldItalic(url);

            // remove <>'s surrounding URL, if present
            if (url.StartsWith("<") && url.EndsWith(">"))
                url = url.Substring(1, url.Length - 2);

            var result = string.Format("<a href=\"{0}\"", url);
            if (!string.IsNullOrEmpty(title))
            {
                title = AttributeEncode(title);
                title = EscapeBoldItalic(title);
                result += string.Format(" title=\"{0}\"", title);
            }

            result += string.Format(">{0}</a>", linkText);
            return result;
        }

        private static readonly Regex _imagesRef = new Regex(
            @"
                    (               # wrap whole match in $1
                    !\[
                        (.*?)       # alt text = $2
                    \]

                    [ ]?            # one optional space
                    (?:\n[ ]*)?     # one optional newline followed by spaces

                    \[
                        (.*?)       # id = $3
                    \]

                    )", RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex _imagesInline = new Regex(
            string.Format(
            @"
              (                     # wrap whole match in $1
                !\[
                    (.*?)           # alt text = $2
                \]
                \s?                 # one optional whitespace character
                \(                  # literal paren
                    [ ]*
                    ({0})           # href = $3
                    [ ]*
                    (               # $4
                    (['""])       # quote char = $5
                    (.*?)           # title = $6
                    \5              # matching quote
                    [ ]*
                    )?              # title is optional
                \)
              )",
            GetNestedParensPattern()),
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled);

        /// <summary>
        /// Turn Markdown image shortcuts into HTML img tags.
        /// </summary>
        /// <remarks>
        /// ![alt text][id]
        /// ![alt text](url "optional title")
        /// </remarks>
        private string DoImages(string text)
        {
            // First, handle reference-style labeled images: ![alt text][id]
            text = _imagesRef.Replace(text, ImageReferenceEvaluator);

            // Next, handle inline images:  ![alt text](url "optional title")
            // Don't forget: encode * and _
            return _imagesInline.Replace(text, ImageInlineEvaluator);
        }

        // This prevents the creation of horribly broken HTML when some syntax ambiguities
        // collide. It likely still doesn't do what the user meant, but at least we're not
        // outputting garbage.
        private static string EscapeImageAltText(string s)
        {
            s = EscapeBoldItalic(s);
            s = Regex.Replace(s, @"[\[\]()]", m => _escapeTable[m.ToString()]);
            return s;
        }

        private string ImageReferenceEvaluator(Match match)
        {
            var wholeMatch = match.Groups[1].Value;
            var altText = match.Groups[2].Value;
            var linkId = match.Groups[3].Value.ToLowerInvariant();

            // for shortcut links like ![this][].
            if (linkId == string.Empty)
                linkId = altText.ToLowerInvariant();

            altText = EscapeImageAltText(AttributeEncode(altText));

            if (!_urls.ContainsKey(linkId)) // If there's no such link ID, leave intact:
                return wholeMatch;

            var url = _urls[linkId];
            url = EncodeProblemUrlChars(url);
            url = EscapeBoldItalic(url);

            _images[altText] = url;

            var result = string.Format("<img src=\"{0}\" alt=\"{1}\"", url, altText);
            if (_titles.ContainsKey(linkId))
            {
                var title = _titles[linkId];
                title = EscapeBoldItalic(title);

                result += string.Format(" title=\"{0}\"", title);
            }

            result += EmptyElementSuffix;

            return result;
        }

        private string ImageInlineEvaluator(Match match)
        {
            var alt = match.Groups[2].Value;
            var url = match.Groups[3].Value;
            var title = match.Groups[6].Value;

            alt = AttributeEncode(alt);
            title = AttributeEncode(title);

            if (url.StartsWith("<") && url.EndsWith(">"))
                url = url.Substring(1, url.Length - 2);    // Remove <>'s surrounding URL, if present
            url = EncodeProblemUrlChars(url);
            url = EscapeBoldItalic(url);

            var sb = new StringBuilder();
            sb.AppendFormat("<img src=\"{0}\" alt=\"{1}\"", url, alt);

            if (!string.IsNullOrEmpty(title))
            {
                title = EscapeBoldItalic(title);
                sb.AppendFormat(" title=\"{0}\"", title);
            }
            sb.Append(EmptyElementSuffix);

            return sb.ToString();
        }

        private static readonly Regex _headerSetext = new Regex(
            @"
                ^(.+?)
                [ ]*
                \n
                (=+|-+)     # $1 = string of ='s or -'s
                [ ]*
                \n+",
            RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private static readonly Regex _headerAtx = new Regex(
            @"
                ^(\#{1,6})  # $1 = string of #'s
                [ ]*
                (.+?)       # $2 = Header text
                [ ]*
                \#*         # optional closing #'s (not counted)
                \n+",
            RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// Turn Markdown headers into HTML header tags
        /// </summary>
        /// <remarks>
        /// Header 1
        /// ========
        ///
        /// Header 2
        /// --------
        ///
        /// # Header 1
        /// ## Header 2
        /// ## Header 2 with closing hashes ##
        /// ...
        /// ###### Header 6
        /// </remarks>
        private string DoHeaders(string text)
        {
            text = _headerSetext.Replace(text, SetextHeaderEvaluator);
            text = _headerAtx.Replace(text, AtxHeaderEvaluator);
            return text;
        }

        private string SetextHeaderEvaluator(Match match)
        {
            var header = match.Groups[1].Value;
            var level = match.Groups[2].Value.StartsWith("=") ? 1 : 2;
            return string.Format("<h{1}>{0}</h{1}>\n\n", RunSpanGamut(header), level);
        }

        private string AtxHeaderEvaluator(Match match)
        {
            var header = match.Groups[2].Value;
            var level = match.Groups[1].Value.Length;
            return string.Format("<h{1}>{0}</h{1}>\n\n", RunSpanGamut(header), level);
        }

        private static readonly Regex _horizontalRules =
            new Regex(
                @"
            ^[ ]{0,3}         # Leading space
                ([-*_])       # $1: First marker
                (?>           # Repeated marker group
                    [ ]{0,2}  # Zero, one, or two spaces.
                    \1        # Marker character
                ){2,}         # Group repeated at least twice
                [ ]*          # Trailing spaces
                $             # End of line.
            ",
                RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// Turn Markdown horizontal rules into HTML hr tags
        /// </summary>
        /// <remarks>
        /// ***
        /// * * *
        /// ---
        /// - - -
        /// </remarks>
        private string DoHorizontalRules(string text)
        {
            return _horizontalRules.Replace(text, "<hr" + EmptyElementSuffix + "\n");
        }

        private static readonly string _wholeList =
            string.Format(
                @"
            (                               # $1 = whole list
              (                             # $2
                [ ]{{0,{1}}}
                ({0})                       # $3 = first list item marker
                [ ]+
              )
              (?s:.+?)
              (                             # $4
                  \z
                |
                  \n{{2,}}
                  (?=\S)
                  (?!                       # Negative lookahead for another list item marker
                    [ ]*
                    {0}[ ]+
                  )
              )
            )",
                string.Format("(?:{0}|{1})", MarkerUl, MarkerOl),
                TabWidth - 1);

        private static readonly Regex _listNested =
            new Regex(
                @"^" + _wholeList,
                RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private static readonly Regex _listTopLevel =
            new Regex(
                @"(?:(?<=\n\n)|\A\n?)" + _wholeList,
                RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// Turn Markdown lists into HTML ul and ol and li tags
        /// </summary>
        private string DoLists(string text)
        {
            // We use a different prefix before nested lists than top-level lists.
            // See extended comment in _ProcessListItems().
            return _listLevel > 0 ? _listNested.Replace(text, ListEvaluator) : _listTopLevel.Replace(text, ListEvaluator);
        }

        private string ListEvaluator(Match match)
        {
            var list = match.Groups[1].Value;
            var listType = Regex.IsMatch(match.Groups[3].Value, MarkerUl) ? "ul" : "ol";

            var result = ProcessListItems(list, listType == "ul" ? MarkerUl : MarkerOl);
            return string.Format("<{0}>\n{1}</{0}>\n", listType, result);
        }

        /// <summary>
        /// Process the contents of a single ordered or unordered list, splitting it
        /// into individual list items.
        /// </summary>
        private string ProcessListItems(string list, string marker)
        {
            // The listLevel global keeps track of when we're inside a list.
            // Each time we enter a list, we increment it; when we leave a list,
            // we decrement. If it's zero, we're not in a list anymore.
            //
            // We do this because when we're not inside a list, we want to treat
            // something like this:
            //
            //    I recommend upgrading to version
            //    8. Oops, now this line is treated
            //    as a sub-list.
            //
            // As a single paragraph, despite the fact that the second line starts
            // with a digit-period-space sequence.
            //
            // Whereas when we're inside a list (or sub-list), that line will be
            // treated as the start of a sub-list. What a kludge, huh? This is
            // an aspect of Markdown's syntax that's hard to parse perfectly
            // without resorting to mind-reading. Perhaps the solution is to
            // change the syntax rules such that sub-lists must start with a
            // starting cardinal number; e.g. "1." or "a.".

            _listLevel++;

            // Trim trailing blank lines:
            list = Regex.Replace(list, @"\n{2,}\z", "\n");

            var pattern = string.Format(
              @"(^[ ]*)                    # leading whitespace = $1
                ({0}) [ ]+                 # list marker = $2
                ((?s:.+?)                  # list item text = $3
                (\n+))      
                (?= (\z | \1 ({0}) [ ]+))", marker);

            var lastItemHadADoubleNewline = false;

            // has to be a closure, so subsequent invocations can share the bool
            MatchEvaluator listItemEvaluator = match =>
            {
                var item = match.Groups[3].Value;

                var endsWithDoubleNewline = item.EndsWith("\n\n");
                var containsDoubleNewline = endsWithDoubleNewline || item.Contains("\n\n");

                if (containsDoubleNewline || lastItemHadADoubleNewline)
                {
                    // we could correct any bad indentation here..
                    item = RunBlockGamut(Outdent(item) + "\n", unhash: false);
                }
                else
                {
                    // recursion for sub-lists
                    item = DoLists(Outdent(item));
                    item = item.TrimEnd('\n');
                    item = RunSpanGamut(item);
                }
                lastItemHadADoubleNewline = endsWithDoubleNewline;
                return string.Format("<li>{0}</li>\n", item);
            };

            list = Regex.Replace(
                list,
                pattern,
                listItemEvaluator,
                RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);

            _listLevel--;
            return list;
        }

        private static readonly Regex _codeBlock =
            new Regex(
                string.Format(
                    @"
                    (?:\n\n|\A\n?)
                    (                        # $1 = the code block -- one or more lines, starting with a space
                    (?:
                        (?:[ ]{{{0}}})       # Lines must start with a tab-width of spaces
                        .*\n+
                    )+
                    )
                    ((?=^[ ]{{0,{0}}}\S)|\Z) # Lookahead for non-space at line-start, or end of doc",
                    TabWidth),
                RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// /// Turn Markdown 4-space indented code into HTML pre code blocks
        /// </summary>
        private string DoCodeBlocks(string text)
        {
            text = _codeBlock.Replace(text, CodeBlockEvaluator);
            return text;
        }

        private string CodeBlockEvaluator(Match match)
        {
            var codeBlock = match.Groups[1].Value;

            codeBlock = EncodeCode(Outdent(codeBlock));
            codeBlock = _newlinesLeadingTrailing.Replace(codeBlock, string.Empty);

            return string.Concat("\n\n<pre><code>", codeBlock, "\n</code></pre>\n\n");
        }

        private static readonly Regex _codeSpan = new Regex(
            @"
                    (?<!\\)   # Character before opening ` can't be a backslash
                    (`+)      # $1 = Opening run of `
                    (.+?)     # $2 = The code block
                    (?<!`)
                    \1
                    (?!`)", RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled);

        /// <summary>
        /// Turn Markdown `code spans` into HTML code tags
        /// </summary>
        private string DoCodeSpans(string text)
        {
            // * You can use multiple backticks as the delimiters if you want to
            //   include literal backticks in the code span. So, this input:
            //
            //   Just type ``foo `bar` baz`` at the prompt.
            //
            //   Will translate to:
            //
            //     <p>Just type <code>foo `bar` baz</code> at the prompt.</p>
            //
            //   There's no arbitrary limit to the number of backticks you
            //   can use as delimters. If you need three consecutive backticks
            //   in your code, use four for delimiters, etc.
            //
            // * You can use spaces to get literal backticks at the edges:
            //
            //     ... type `` `bar` `` ...
            //
            //   Turns to:
            //
            //     ... type <code>`bar`</code> ...

            return _codeSpan.Replace(text, CodeSpanEvaluator);
        }

        private string CodeSpanEvaluator(Match match)
        {
            var span = match.Groups[2].Value;
            span = Regex.Replace(span, @"^[ ]*", string.Empty); // leading whitespace
            span = Regex.Replace(span, @"[ ]*$", string.Empty); // trailing whitespace
            span = EncodeCode(span);
            span = SaveFromAutoLinking(span); // to prevent auto-linking. Not necessary in code *blocks*, but in code spans.

            return string.Concat("<code>", span, "</code>");
        }

        private static readonly Regex _bold =
            new Regex(
                @"(\*\*|__) (?=\S) (.+?[*_]*) (?<=\S) \1",
                RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex _strictBold =
            new Regex(
                @"([\W_]|^) (\*\*|__) (?=\S) ([^\r]*?\S[\*_]*) \2 ([\W_]|$)",
                RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex _italic =
            new Regex(
                @"(\*|_) (?=\S) (.+?) (?<=\S) \1",
                RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex _strictItalic =
            new Regex(
                @"([\W_]|^) (\*|_) (?=\S) ([^\r\*_]*?\S) \2 ([\W_]|$)",
                RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled);

        /// <summary>
        /// Turn Markdown *italics* and **bold** into HTML strong and em tags
        /// </summary>
        private string DoItalicsAndBold(string text)
        {
            // <strong> must go first, then <em>
            if (StrictBoldItalic)
            {
                text = _strictBold.Replace(text, "$1<strong>$3</strong>$4");
                text = _strictItalic.Replace(text, "$1<em>$3</em>$4");
            }
            else
            {
                text = _bold.Replace(text, "<strong>$2</strong>");
                text = _italic.Replace(text, "<em>$2</em>");
            }
            return text;
        }

        /// <summary>
        /// Turn markdown line breaks (two space at end of line) into HTML break tags
        /// </summary>
        private string DoHardBreaks(string text)
        {
            return Regex.Replace(text, AutoNewLines ? @"\n" : @" {2,}\n", string.Format("<br{0}\n", EmptyElementSuffix));
        }

        private static readonly Regex _blockquote =
            new Regex(
                @"
            (                           # Wrap whole match in $1
                (
                ^[ ]*>[ ]?              # '>' at the start of a line
                    .+\n                # rest of the first line
                (.+\n)*                 # subsequent consecutive lines
                \n*                     # blanks
                )+
            )",
                RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.Compiled);

        /// <summary>
        /// Turn Markdown > quoted blocks into HTML blockquote blocks
        /// </summary>
        private string DoBlockQuotes(string text)
        {
            return _blockquote.Replace(text, BlockQuoteEvaluator);
        }

        private string BlockQuoteEvaluator(Match match)
        {
            var bq = match.Groups[1].Value;

            bq = Regex.Replace(bq, @"^[ ]*>[ ]?", string.Empty, RegexOptions.Multiline);       // trim one level of quoting
            bq = Regex.Replace(bq, @"^[ ]+$", string.Empty, RegexOptions.Multiline);           // trim whitespace-only lines
            bq = RunBlockGamut(bq);                                                  // recurse

            bq = Regex.Replace(bq, @"^", "  ", RegexOptions.Multiline);

            // These leading spaces screw with <pre> content, so we need to fix that:
            bq = Regex.Replace(bq, @"(\s*<pre>.+?</pre>)", BlockQuoteEvaluator2, RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline);

            bq = string.Format("<blockquote>\n{0}\n</blockquote>", bq);
            var key = GetHashKey(bq, true);
            _htmlBlocks[key] = bq;

            return "\n\n" + key + "\n\n";
        }

        private static string BlockQuoteEvaluator2(Match match)
        {
            return Regex.Replace(match.Groups[1].Value, @"^  ", string.Empty, RegexOptions.Multiline);
        }

        private static readonly Regex _article =
            new Regex(
                @"
            (                           # Wrap whole match in $1
                (
                ^[ ]*\$[ ]?             # '$' at the start of a line
                    .+\n                # rest of the first line
                (.+\n)*                 # subsequent consecutive lines
                \n*                     # blanks
                )+
            )", RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.Compiled);

        /// <summary>
        /// Turn Markdown $ quoted blocks into HTML article blocks
        /// </summary>
        private string DoArticles(string text)
        {
            return SupportArticles ? _article.Replace(text, ArticleEvaluator) : text;
        }

        private string ArticleEvaluator(Match match)
        {
            var article = match.Groups[1].Value;

            article = Regex.Replace(article, @"^[ ]*\$[ ]?", string.Empty, RegexOptions.Multiline);      // trim one level of quoting
            article = Regex.Replace(article, @"^[ ]+$", string.Empty, RegexOptions.Multiline);           // trim whitespace-only lines
            article = RunBlockGamut(article);                                                  // recurse

            // These leading spaces screw with <pre> content, so we need to fix that:
            article = Regex.Replace(article, @"(\s*<pre>.+?</pre>)", ArticleEvaluator2, RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline);

            _articles.Add(article);

            article = string.Format("<article>\n{0}\n</article>", article);
            var key = GetHashKey(article, true);
            _htmlBlocks[key] = article;

            return "\n\n" + key + "\n\n";
        }

        private static string ArticleEvaluator2(Match match)
        {
            return Regex.Replace(match.Groups[1].Value, @"^  ", string.Empty, RegexOptions.Multiline);
        }

        private static readonly Regex _autolinkBare =
            new Regex(@"(^|\s)(https?|ftp)(://[-A-Z0-9+&@#/%?=~_|\[\]\(\)!:,\.;]*[-A-Z0-9+&@#/%=~_|\[\]])($|\W)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Turn angle-delimited URLs into HTML anchor tags
        /// </summary>
        /// <remarks>
        /// &lt;http://www.example.com&gt;
        /// </remarks>
        private string DoAutoLinks(string text)
        {
            if (AutoHyperlink)
            {
                // fixup arbitrary URLs by adding Markdown < > so they get linked as well
                // note that at this point, all other URL in the text are already hyperlinked as <a href=""></a>
                // *except* for the <http://www.foo.com> case
                text = _autolinkBare.Replace(text, @"$1<$2$3>$4");
            }

            // Hyperlinks: <http://foo.com>
            text = Regex.Replace(text, "<((https?|ftp):[^'\">\\s]+)>", HyperlinkEvaluator);
            if (LinkEmails)
            {
                // Email addresses: <address@domain.foo>
                const string pattern = @"<
                      (?:mailto:)?
                      (
                        [-.\w]+
                        \@
                        [-a-z0-9]+(\.[-a-z0-9]+)*\.[a-z]+
                      )
                      >";
                text = Regex.Replace(text, pattern, EmailEvaluator, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            }

            return text;
        }

        private static string HyperlinkEvaluator(Match match)
        {
            var link = match.Groups[1].Value;
            return string.Format("<a href=\"{0}\">{0}</a>", link);
        }

        private static string EmailEvaluator(Match match)
        {
            var email = Unescape(match.Groups[1].Value);

            // Input: an email address, e.g. "foo@example.com"
            //
            // Output: the email address as a mailto link, with each character
            //         of the address encoded as either a decimal or hex entity, in
            //         the hopes of foiling most address harvesting spam bots. E.g.:
            //
            // <a href="&#x6D;&#97;&#105;&#108;&#x74;&#111;:&#102;&#111;&#111;&#64;&#101;
            //          x&#x61;&#109;&#x70;&#108;&#x65;&#x2E;&#99;&#111;&#109;">&#102;&#111;&#111;
            //          &#64;&#101;x&#x61;&#109;&#x70;&#108;&#x65;&#x2E;&#99;&#111;&#109;</a>
            //
            // Based by a filter by Matthew Wickline, posted to the BBEdit-Talk
            // mailing list: <http://tinyurl.com/yu7ue>
            email = "mailto:" + email;

            // leave ':' alone (to spot mailto: later)
            email = EncodeEmailAddress(email);

            email = string.Format("<a href=\"{0}\">{0}</a>", email);

            // strip the mailto: from the visible part
            email = Regex.Replace(email, "\">.+?:", "\">");
            return email;
        }

        private static readonly Regex _outDent =
            new Regex(@"^[ ]{1," + TabWidth + @"}", RegexOptions.Multiline | RegexOptions.Compiled);

        /// <summary>
        /// Remove one level of line-leading spaces
        /// </summary>
        private static string Outdent(string block)
        {
            return _outDent.Replace(block, string.Empty);
        }

        #region Encoding and Normalization

        /// <summary>
        /// encodes email address randomly
        /// roughly 10% raw, 45% hex, 45% dec
        /// note that @ is always encoded and : never is
        /// </summary>
        private static string EncodeEmailAddress(string addr)
        {
            var sb = new StringBuilder(addr.Length * 5);
            var rand = new Random();
            foreach (var c in addr)
            {
                var r = rand.Next(1, 100);
                if ((r > 90 || c == ':') && c != '@')
                    sb.Append(c);                         // m
                else if (r < 45)
                    sb.AppendFormat("&#x{0:x};", (int)c); // &#x6D
                else
                    sb.AppendFormat("&#{0};", (int)c);    // &#109
            }
            return sb.ToString();
        }

        private static readonly Regex _codeEncoder = new Regex(@"&|<|>|\\|\*|_|\{|\}|\[|\]", RegexOptions.Compiled);

        /// <summary>
        /// Encode/escape certain Markdown characters inside code blocks and spans where they are literals
        /// </summary>
        private static string EncodeCode(string code)
        {
            return _codeEncoder.Replace(code, EncodeCodeEvaluator);
        }

        private static string EncodeCodeEvaluator(Match match)
        {
            switch (match.Value)
            {
                // Encode all ampersands; HTML entities are not
                // entities within a Markdown code span.
                case "&":
                    return "&amp;";

                // Do the angle bracket song and dance
                case "<":
                    return "&lt;";
                case ">":
                    return "&gt;";

                // escape characters that are magic in Markdown
                default:
                    return _escapeTable[match.Value];
            }
        }

        private static readonly Regex _amps =
            new Regex(@"&(?!((#[0-9]+)|(#[xX][a-fA-F0-9]+)|([a-zA-Z][a-zA-Z0-9]*));)", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        private static readonly Regex _angles =
            new Regex(@"<(?![A-Za-z/?\$!])", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Encode any ampersands (that aren't part of an HTML entity) and left or right angle brackets
        /// </summary>
        private static string EncodeAmpsAndAngles(string s)
        {
            s = _amps.Replace(s, "&amp;");
            s = _angles.Replace(s, "&lt;");
            return s;
        }

        private static readonly Regex _backslashEscapes;

        /// <summary>
        /// Encodes any escaped characters such as \`, \*, \[ etc
        /// </summary>
        private static string EscapeBackslashes(string s)
        {
            return _backslashEscapes.Replace(s, EscapeBackslashesEvaluator);
        }

        private static string EscapeBackslashesEvaluator(Match match)
        {
            return _backslashEscapeTable[match.Value];
        }

        private static readonly Regex _unescapes = new Regex("\x1A" + "E\\d+E", RegexOptions.Compiled);

        /// <summary>
        /// swap back in all the special characters we've hidden
        /// </summary>
        private static string Unescape(string s)
        {
            return _unescapes.Replace(s, UnescapeEvaluator);
        }

        private static string UnescapeEvaluator(Match match)
        {
            return _invertedEscapeTable[match.Value];
        }

        /// <summary>
        /// escapes Bold [ * ] and Italic [ _ ] characters
        /// </summary>
        private static string EscapeBoldItalic(string s)
        {
            s = s.Replace("*", _escapeTable["*"]);
            s = s.Replace("_", _escapeTable["_"]);
            return s;
        }

        private static string AttributeEncode(string s)
        {
            return s.Replace(">", "&gt;").Replace("<", "&lt;").Replace("\"", "&quot;");
        }

        private static readonly char[] _problemUrlChars = @"""'*()[]$:_".ToCharArray();

        /// <summary>
        /// hex-encodes some unusual "problem" chars in URLs to avoid URL detection problems
        /// </summary>
        private string EncodeProblemUrlChars(string url)
        {
            if (!EncodeProblemUrlCharacters)
                return url;

            var sb = new StringBuilder(url.Length);
            for (var i = 0; i < url.Length; i++)
            {
                var c = url[i];
                var encode = Array.IndexOf(_problemUrlChars, c) != -1;
                if (encode && c == ':' && i < url.Length - 1)
                    encode = url[i + 1] != '/' && !(url[i + 1] >= '0' && url[i + 1] <= '9');

                if (encode)
                    sb.Append("%" + string.Format("{0:x}", (byte)c));
                else
                    sb.Append(c);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Within tags -- meaning between &lt; and &gt; -- encode [\ ` * _] so they
        /// don't conflict with their use in Markdown for code, italics and strong.
        /// /// We're replacing each such character with its corresponding hash
        /// value; this is likely overkill, but it should prevent us from colliding
        /// with the escape values by accident.
        /// </summary>
        private string EscapeSpecialCharsWithinTagAttributes(string text)
        {
            var tokens = TokenizeHtml(text);

            // now, rebuild text from the tokens
            var sb = new StringBuilder(text.Length);
            foreach (var token in tokens)
            {
                var value = token.Value;

                if (token.Type == TokenType.Tag)
                {
                    value = value.Replace(@"\", _escapeTable[@"\"]);

                    // escape slashes in comments to prevent autolinking there -- http://meta.stackoverflow.com/questions/95987/html-comment-containing-url-breaks-if-followed-by-another-html-comment
                    if (AutoHyperlink && value.StartsWith("<!"))
                        value = value.Replace("/", _escapeTable["/"]);

                    value = Regex.Replace(value, "(?<=.)</?code>(?=.)", _escapeTable[@"`"]);
                    value = EscapeBoldItalic(value);
                }

                sb.Append(value);
            }

            return sb.ToString();
        }

        /// <summary>
        /// convert all tabs to _tabWidth spaces;
        /// standardizes line endings from DOS (CR LF) or Mac (CR) to UNIX (LF);
        /// makes sure text ends with a couple of newlines;
        /// removes any blank lines (only spaces) in the text
        /// </summary>
        private static string Normalize(string text)
        {
            var output = new StringBuilder(text.Length);
            var line = new StringBuilder();
            var valid = false;

            for (var i = 0; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case '\n':
                        if (valid)
                        {
                            output.Append(line);
                        }

                        output.Append('\n');
                        line.Length = 0; valid = false;
                        break;
                    case '\r':
                        if ((i < text.Length - 1) && (text[i + 1] != '\n'))
                        {
                            if (valid)
                            {
                                output.Append(line);
                            }

                            output.Append('\n');
                            line.Length = 0;
                            valid = false;
                        }
                        break;
                    case '\t':
                        var width = TabWidth - (line.Length % TabWidth);
                        for (var k = 0; k < width; k++)
                            line.Append(' ');
                        break;
                    case '\x1A':
                        break;
                    default:
                        if (!valid && text[i] != ' ')
                        {
                            valid = true;
                        }

                        line.Append(text[i]);
                        break;
                }
            }

            if (valid)
            {
                output.Append(line);
            }

            output.Append('\n');

            // add two newlines to the end before return
            return output.Append("\n\n").ToString();
        }

        #endregion

        /// <summary>
        /// this is to emulate what's evailable in PHP
        /// </summary>
        private static string RepeatString(string text, int count)
        {
            var sb = new StringBuilder(text.Length * count);
            for (var i = 0; i < count; i++)
                sb.Append(text);
            return sb.ToString();
        }
    }
}
#pragma warning restore SA1204
