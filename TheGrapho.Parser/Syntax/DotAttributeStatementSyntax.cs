﻿// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Diagnostics.CodeAnalysis;

namespace TheGrapho.Parser.Syntax
{
    public sealed class DotAttributeStatementSyntax : DotSyntax
    {
        public DotAttributeStatementSyntax(
            [DisallowNull] KeywordSyntax graphNodeOrEdge,
            [DisallowNull] DotAttributeListSyntax attributes) : base(
            SyntaxKind.DotAttributeStatement,
            graphNodeOrEdge?.Start ?? 0,
            (graphNodeOrEdge?.FullWidth ?? 0) + (attributes?.FullWidth ?? 0),
            new SyntaxNode?[] {graphNodeOrEdge, attributes})
        {
            Keyword = graphNodeOrEdge ?? throw new ArgumentNullException(nameof(graphNodeOrEdge));
            Attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
        }

        [NotNull] public KeywordSyntax Keyword { get; }
        [NotNull] public DotAttributeListSyntax Attributes { get; }

        [return: MaybeNull]
        public override TResult Accept<TResult>([DisallowNull] DotSyntaxVisitor<TResult> syntaxVisitor)
        {
            if (syntaxVisitor == null) throw new ArgumentNullException(nameof(syntaxVisitor));
            return syntaxVisitor.VisitAttributeStatement(this);
        }

        public override void Accept([DisallowNull] DotSyntaxVisitor syntaxVisitor)
        {
            if (syntaxVisitor == null) throw new ArgumentNullException(nameof(syntaxVisitor));
            syntaxVisitor.VisitAttributeStatement(this);
        }

        [return: NotNull]
        public override string ToString() =>
            $"{base.ToString()}, {nameof(Keyword)}: {Keyword}, {nameof(Attributes)}: {Attributes}";
    }
}