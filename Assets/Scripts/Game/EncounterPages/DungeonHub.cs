﻿using Scripts.Game.Serialized;
using Scripts.Model.Characters;
using Scripts.Model.Interfaces;
using Scripts.Model.Pages;
using System.Collections.Generic;

namespace Scripts.Game.Pages {
    public class DungeonHub : PageGroup {
        private readonly Party party;
        private readonly Flags flags;
        private readonly Page previous;

        public DungeonHub(Page previous, Party party, Flags flags) : base(new Page("Explore")) {
            var buttons = new List<IButtonable>();
            this.party = party;
            this.flags = flags;
            this.previous = previous;

            Root.Icon = Util.GetSprite("walking-boot");
            Root.AddCharacters(Side.LEFT, party);
            Root.Condition = PageUtil.GetVisitProcessCondition(flags, party);

            buttons.Add(PageUtil.GenerateBack(previous));

            Get(ROOT_INDEX).Actions = buttons;
            Get(ROOT_INDEX).OnEnter = () => {
                Get(ROOT_INDEX)
                .AddText(
                    "Where would you like to explore?"
                    );
            };
        }
    }
}