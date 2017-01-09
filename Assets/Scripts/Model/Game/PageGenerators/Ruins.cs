﻿using Scripts.Model.Characters;
using Scripts.Model.Characters.Named;
using Scripts.Model.World.Pages;
using Scripts.Model.Pages;
using System;
using Scripts.Model.World.Flags;

namespace Scripts.Model.World.PageGenerators.Named {

    public class Ruins : PageGenerator {
        private const string BATTLE_MUSIC = "Hero Immortal";

        private Page camp;
        private Party party;

        public Ruins(EventFlags flags, Page camp, Party party) : base("Ruins", "The ruins of some fallen civilization bordering the edge of the world.") {
            this.camp = camp;
            this.party = party;
            encounters.Add(
                TypicalBattle(
                    "A human in a white priestly gown blocks your way. Their clothing is in perfect condition.",
                    Rarity.UNCOMMON,
                    new Regenerator()
                    )
                );

            encounters.Add(
                TypicalBattle(
                    "A human knight in white armor blocks your way, drawing their sword. Their armor looks completely new and untouched.",
                    Rarity.COMMON,
                    new Lasher()
                    )
            );

            encounters.Add(
                TypicalBattle(
                    "A human knight and bishop pair block the way.",
                    Rarity.RARE,
                    new Lasher(),
                    new Regenerator()
                    )
            );

            encounters.Add(
                    new Encounter(
                    () => new ReadPage(party,
                        "Deep in the ruins, you come across a lengthy bridge across the edge of the world. Way off in the distance,"
                        + " you see a temple aligned with the bridge.\nYou discovered the Temple!\n(Visit this location using Places back at camp.)",
                        "Ruins",
                        () => flags.Bools[Flag.DISCOVERED_TEMPLE] = true,
                        camp
                        ),
                    Rarity.UNCOMMON,
                    () => !flags.Bools[Flag.DISCOVERED_TEMPLE])
                );

            encounters.Add(
                    new Encounter(
                        () => new ShopPage(flags, camp, party),
                        Rarity.COMMON,
                        () => flags.Ints[Flag.SHOPKEEPER_STATUS] == Flag.SHOPKEEPER_NEUTRAL || flags.Ints[Flag.SHOPKEEPER_STATUS] == Flag.SHOPKEEPER_FRIENDLY)
                );

            encounters.Add(
                new Encounter(
                    () => new BattlePage(party,
                    new BattleResult(camp),
                    new BattleResult(camp),
                    "",
                    "Ruins",
                    "You come across Maple. She doesn't look too happy.", new Character[] { new Shopkeeper(flags) }),
                    Rarity.COMMON,
                    () => flags.Ints[Flag.SHOPKEEPER_STATUS] == Flag.SHOPKEEPER_ENEMY)
                );
        }

        private Encounter TypicalBattle(string text, Rarity rarity, Func<bool> isEnabled, params Character[] enemies) {
            return new Encounter(
                () => new BattlePage(party, new BattleResult(camp), new BattleResult(camp), BATTLE_MUSIC, "Ruins", text, enemies),
                rarity,
                isEnabled);
        }

        private Encounter TypicalBattle(string text, Rarity rarity, params Character[] enemies) {
            return new Encounter(
                () => new BattlePage(party, new BattleResult(camp), new BattleResult(camp), BATTLE_MUSIC, "Ruins", text, enemies),
                rarity);
        }
    }
}