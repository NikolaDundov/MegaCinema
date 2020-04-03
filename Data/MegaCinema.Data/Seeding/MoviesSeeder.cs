namespace MegaCinema.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MegaCinema.Data.Models;
    using MegaCinema.Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;

    public class MoviesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Movies.AnyAsync())
            {
                return;
            }

            var movieJojo = new Movie
            {
                Title = "Jojo",
                Genre = GenreType.Comedy,
                Country = Country.USA,
                Description = "A World War II satire that follows a lonely German " +
                "boy named Jojo (Roman Griffin Davis) whose world view is turned upside " +
                "down when he discovers his single mother (Scarlett Johansson) is hiding a " +
                "young Jewish girl (Thomasin McKenzie) in their attic. Aided only by his " +
                "idiotic imaginary friend, Adolf Hitler (Taika Waititi), Jojo must confront " +
                "his blind nationalism.",
                Director = "Taika Waititi",
                Actors = "Roman Griffin Davis, Thomasin McKenzie, Scarlett Johansson, Taika Waititi",
                Duration = new TimeSpan(1, 45, 52),
                Poster = "https://i.imgur.com/F7gxP9z.jpg",
                Language = Language.English,
                Rating = MPAARating.G,
                Trailer = "tL4McUzXfFI",
                ReleaseDate = new DateTime(2020, 01, 15),
                Score = 7.1,
            };

            var movieSoic = new Movie
            {
                Title = "Sonic the Hedgehog",
                Genre = GenreType.Adventure,
                Country = Country.USA,
                Description = "After discovering a small, blue, fast hedgehog, a small-town police " +
                "officer must help it defeat an evil genius who wants to do experiments on it.",
                Director = "Jeff Fowler",
                Actors = "Ben Schwartz, James Marsden, Jim Carrey, Tika Sumpter",
                Duration = new TimeSpan(1, 39, 12),
                Poster = "https://i.imgur.com/9QFEGbr.jpg",
                Language = Language.English,
                Rating = MPAARating.G,
                Trailer = "szby7ZHLnkA",
                ReleaseDate = new DateTime(2020, 02, 14),
                Score = 7.2,
            };

            var movieJumanji = new Movie
            {
                Title = "Jumanji: The Next Level",
                Genre = GenreType.Adventure,
                Country = Country.USA,
                Description = "In Jumanji: The Next Level, the gang is back but the game has changed. " +
                "As they return to rescue one of their own, the players will have to brave parts unknown" +
                " from arid deserts to snowy mountains, to escape the world's most dangerous game.",
                Director = "Jake Kasdan",
                Actors = "Dwayne Johnson, Kevin Hart, Jack Black, Karen Gillan",
                Duration = new TimeSpan(2, 12, 05),
                Poster = "https://i.imgur.com/dAkfgZg.jpg",
                Language = Language.English,
                Rating = MPAARating.PG13,
                Trailer = "rBxcF-r9Ibs",
                ReleaseDate = new DateTime(2019, 12, 13),
                Score = 7.7,
            };

            var movieTheWayBack = new Movie
            {
                Title = "The Way Back",
                Genre = GenreType.Drama,
                Country = Country.USA,
                Description = "Jack Cunningham was an HS basketball phenom who walked away from the game, forfeiting " +
                "his future. Years later, when he reluctantly accepts a coaching job at his alma mater, he may get " +
                "one last shot at redemption.",
                Actors = "Ben Affleck, Al Madrigal, Janina Gavankar, Michaela Watkins",
                Director = "Gavin O'Connor",
                Duration = new TimeSpan(1, 48, 05),
                Poster = "https://i.imgur.com/lKpiwAA.jpg",
                Language = Language.English,
                Rating = MPAARating.R,
                Trailer = "GhtTc7R8yBk",
                ReleaseDate = new DateTime(2020, 03, 06),
                Score = 7.1,
            };

            var movieBadBoysForLife = new Movie
            {
                Title = "Bad Boys for Life",
                Genre = GenreType.Action,
                Country = Country.USA,
                Description = "Marcus and Mike have to confront new issues (career changes and midlife crises), " +
                "as they join the newly created elite team AMMO of the Miami police department to take down the " +
                "ruthless Armando Armas, the vicious leader of a Miami drug cartel.",
                Actors = "Will Smith, Martin Lawrence, Vanessa Hudgens, Alexander Ludwig",
                Duration = new TimeSpan(2, 04, 05),
                Director = "Adil El Arbi, Bilall Fallah",
                Poster = "https://i.imgur.com/DIaU9e1.jpg",
                Language = Language.English,
                Rating = MPAARating.R,
                Trailer = "jKCj3XuPG8M",
                ReleaseDate = new DateTime(2020, 01, 17),
                Score = 7.2,
            };

            var movieEmma = new Movie
            {
                Title = "Emma.",
                Genre = GenreType.Drama,
                Country = Country.UK,
                Description = "Jane Austen's beloved comedy about finding your equal and earning your happy " +
                "ending, is reimagined in this. Handsome, clever, and rich, Emma Woodhouse is a restless " +
                "queen bee without rivals in her sleepy little town. In this glittering satire of social " +
                "class and the pain of growing up, Emma must adventure through misguided matches and romantic " +
                "missteps to find the love that has been there all along.",
                Actors = "Anya Taylor-Joy, Johnny Flynn, Bill Nighy, Mia Goth",
                Duration = new TimeSpan(2, 05, 05),
                Director = "Autumn de Wilde",
                Poster = "https://i.imgur.com/FhaxLHF.jpg",
                Language = Language.English,
                Rating = MPAARating.PG,
                Trailer = "qsOwj0PR5Sk",
                ReleaseDate = new DateTime(2020, 03, 06),
                Score = 6.8,
            };

            var movieTheInvisibleMan = new Movie
            {
                Title = "The Invisible Man",
                Genre = GenreType.Horror,
                Country = Country.USA,
                Description = "When Cecilia's abusive ex takes his own life and leaves her his fortune, " +
                "she suspects his death was a hoax. As a series of coincidences turn lethal, Cecilia " +
                "works to prove that she is being hunted by someone nobody can see.",
                Actors = "Elisabeth Moss, Oliver Jackson-Cohen, Harriet Dyer, Aldis Hodge",
                Duration = new TimeSpan(2, 04, 25),
                Director = "Leigh Whannell",
                Poster = "https://i.imgur.com/wZm1n8b.jpg",
                Language = Language.English,
                Rating = MPAARating.R,
                Trailer = "WO_FJdiY9dA",
                ReleaseDate = new DateTime(2020, 02, 28),
                Score = 7.2,
            };

            var movieTheSpenserConfidential = new Movie
            {
                Title = "Spenser Confidential",
                Genre = GenreType.Drama,
                Country = Country.USA,
                Description = "Spenser (Mark Wahlberg) - an ex-cop better known for making trouble " +
                "than solving it - just got out of prison. " +
                "But first he gets roped into helping his old boxing coach and mentor, " +
                "Henry (Alan Arkin), with a promising amateur. That's Hawk (Winston Duke), a " +
                "brash, no-nonsense MMA fighter convinced he'll be a tougher opponent than " +
                "Spenser ever was. When two of Spenser's former colleagues turn up murdered, " +
                "he recruits Hawk to help him investigate and bring the justice.",
                Actors = "Mark Wahlberg, Winston Duke, Alan Arkin, Iliza Shlesinger",
                Duration = new TimeSpan(1, 52, 25),
                Director = "Peter Berg",
                Poster = "https://i.imgur.com/DYiC1L0.jpg",
                Language = Language.English,
                Rating = MPAARating.R,
                Trailer = "bgKEoHNi3Uc",
                ReleaseDate = new DateTime(2020, 03, 06),
                Score = 6.1,
            };

            var movieTheGentlemen = new Movie
            {
                Title = "The Gentlemen",
                Genre = GenreType.Action,
                Country = Country.USA,
                Description = "An American expat tries to sell off his highly profitable marijuana empire in London, triggering plots, " +
                "schemes, bribery and blackmail in an attempt to steal his domain out from under him.",
                Actors = "Matthew McConaughey, Charlie Hunnam, Michelle Dockery, Jeremy Strong",
                Duration = new TimeSpan(1, 53, 00),
                Director = "Guy Ritchie",
                Poster = "https://i.imgur.com/hNYqweg.jpg",
                Language = Language.English,
                Rating = MPAARating.R,
                Trailer = "Ify9S7hj480",
                ReleaseDate = new DateTime(2020, 01, 24),
                Score = 7.6,
            };

            var movieBloodshot = new Movie
            {
                Title = "Bloodshot",
                Genre = GenreType.Action,
                Country = Country.USA,
                Description = "Ray Garrison, an elite soldier who was killed in battle, " +
                "is brought back to life by an advanced technology that gives him the ability " +
                "of super human strength and fast healing. With his new abilities, he goes after " +
                "the man who killed his wife, or at least, who he believes killed his wife.",
                Actors = "Vin Diesel, Eiza González, Sam Heughan, Toby Kebbell",
                Duration = new TimeSpan(1, 49, 00),
                Director = "Dave Wilson",
                Poster = "https://i.imgur.com/nFDAf9i.jpg",
                Language = Language.English,
                Rating = MPAARating.PG13,
                Trailer = "vOUVVDWdXbo",
                ReleaseDate = new DateTime(2020, 03, 13),
                Score = 7.5,
            };

            var movieTheClimb = new Movie
            {
                Title = "The Climb",
                Genre = GenreType.Comedy,
                Country = Country.USA,
                Description = "Kyle and Mike are best friends who share a close bond - until " +
                "Mike sleeps with Kyle's fiancée. The Climb is about a tumultuous but enduring " +
                "relationship between two men across many years of laughter, heartbreak and rage. " +
                "It is also the story of real-life best friends who turn their profound connection " +
                "into a rich, humane and frequently uproarious film about the boundaries (or lack thereof) " +
                "in all close friendships.",
                Actors = "Michael Angelo Covino, Kyle Marvin, Gayle Rankin, Judith Godrèche",
                Duration = new TimeSpan(1, 34, 00),
                Director = "Michael Angelo Covino",
                Poster = "https://i.imgur.com/tasgXlB.jpg",
                Language = Language.English,
                Rating = MPAARating.R,
                Trailer = "Mr4MKhV5QVw",
                ReleaseDate = new DateTime(2020, 03, 15),
                Score = 7.2,
            };

            var movieNeverRarelySometimesAlways = new Movie
            {
                Title = "Never Rarely Sometimes Always",
                Genre = GenreType.Drama,
                Country = Country.USA,
                Description = "Inseparable best friends and cousins Autumn and Skylar precariously " +
                "navigate the vulnerability of female adolescence in rural Pennsylvania. When Autumn " +
                "mysteriously falls pregnant, she's confronted by conservative legislation without " +
                "mercy for blue-collar women seeking an abortion. With Skylar's unfailing support " +
                "and bold resourcefulness, money to fund the procedure is secured and the duo board a " +
                "bus bound for New York",
                Actors = "Ryan Eggold, Talia Ryder, Sidney Flanigan, Théodore Pellerin",
                Duration = new TimeSpan(1, 41, 00),
                Director = "Eliza Hittman",
                Poster = "https://i.imgur.com/RwpjEdh.jpg",
                Language = Language.English,
                Rating = MPAARating.PG13,
                Trailer = "hjw_QTKr2rc",
                ReleaseDate = new DateTime(2020, 03, 13),
                Score = 6.5,
            };

            var movieDeerskin = new Movie
            {
                Title = "Deerskin",
                Genre = GenreType.Horror,
                Country = Country.France,
                Description = "A man's obsession with his designer deerskin jacket causes him to " +
                "blow his life savings and turn to crime.",
                Actors = "Jean Dujardin, Adèle Haenel, Albert Delpy, Coralie Russier",
                Duration = new TimeSpan(1, 17, 00),
                Director = "Quentin Dupieux",
                Poster = "https://i.imgur.com/FzspFny.jpg",
                Language = Language.French,
                Rating = MPAARating.PG13,
                Trailer = "vVT4jlEJYQA",
                ReleaseDate = new DateTime(2020, 03, 20),
                Score = 7.1,
            };

            var movieMulan = new Movie
            {
                Title = "Mulan",
                Genre = GenreType.Adventure,
                Country = Country.USA,
                Description = "A young Chinese maiden disguises herself as a male warrior in order to save her father. A live-action feature film " +
                "based on Disney's 'Mulan.'",
                Actors = "Yifei Liu, Donnie Yen, Jet Li, Li Gong",
                Duration = new TimeSpan(1, 38, 00),
                Director = "Niki Caro",
                Poster = "https://i.imgur.com/t64O6Q5.jpg",
                Language = Language.English,
                Rating = MPAARating.PG13,
                Trailer = "KK8FHdFluOQ",
                ReleaseDate = new DateTime(2020, 03, 27),
                Score = 6.2,
            };

            var movieImpracticalJokersTheMovie = new Movie
            {
                Title = "Impractical Jokers: The Movie",
                Genre = GenreType.Comedy,
                Country = Country.USA,
                Description = "The story of a humiliating high school mishap from 1992 that sends the " +
                "Impractical Jokers on the road competing in hidden-camera challenges for the chance " +
                "to turn back the clock and redeem three of the four Jokers.",
                Actors = "Brian Quinn, Joe Gatto, James Murray, Sal Vulcano",
                Duration = new TimeSpan(1, 33, 00),
                Director = "Chris Henchy",
                Poster = "https://i.imgur.com/0Js5dOj.jpg",
                Language = Language.English,
                Rating = MPAARating.PG13,
                Trailer = "PohdpOp-JFE",
                ReleaseDate = new DateTime(2020, 02, 27),
                Score = 4.8,
            };

            var movieTheHunt = new Movie
            {
                Title = "The Hunt",
                Genre = GenreType.Horror,
                Country = Country.USA,
                Description = "Twelve strangers wake up in a clearing. They don't know where they are, " +
                "or how they got there. They don't know they've been chosen - for a very specific " +
                "purpose - The Hunt.",
                Actors = "Betty Gilpin, Hilary Swank, Ike Barinholtz, Wayne Duvall",
                Duration = new TimeSpan(1, 29, 00),
                Director = "Craig Zobel",
                Poster = "https://i.imgur.com/K3KHFco.jpg",
                Language = Language.English,
                Rating = MPAARating.R,
                Trailer = "sowGYbxTPgU",
                ReleaseDate = new DateTime(2020, 03, 13),
                Score = 5.8,
            };

            var movieIStillBelieve = new Movie
            {
                Title = "I Still Believe",
                Genre = GenreType.Drama,
                Country = Country.USA,
                Description = "The true-life story of Christian music star Jeremy Camp and his journey of love " +
                "and loss that looks to prove there is always hope.",
                Actors = "Britt Robertson, K.J. Apa, Melissa Roxburgh, Gary Sinise",
                Duration = new TimeSpan(1, 55, 00),
                Director = "Andrew Erwin",
                Poster = "https://i.imgur.com/IMgzNQM.jpg",
                Language = Language.English,
                Rating = MPAARating.PG,
                Trailer = "YnxHyBbYwQQ",
                ReleaseDate = new DateTime(2020, 03, 15),
                Score = 6.1,
            };

            var movieInsidetheRaine = new Movie
            {
                Title = "Inside the Rain",
                Genre = GenreType.Drama,
                Country = Country.USA,
                Description = "Facing expulsion from college over a misunderstanding, " +
                "a bipolar student indulges his misery at a strip club where he befriends " +
                "a gorgeous, intelligent, outrageous woman and they hatch a madcap " +
                "scheme to prove his innocence.",
                Actors = "Rosie Perez, Eric Roberts, Aaron Fisher, Ellen Toland",
                Duration = new TimeSpan(1, 32, 00),
                Director = "Aaron Fisher",
                Poster = "https://i.imgur.com/GeWczCx.jpg",
                Language = Language.English,
                Rating = MPAARating.PG,
                Trailer = "4RxsUQlOa3M",
                ReleaseDate = new DateTime(2020, 03, 20),
                Score = 6.3,
            };

            await dbContext.Movies.AddAsync(movieJojo);
            await dbContext.Movies.AddAsync(movieSoic);
            await dbContext.Movies.AddAsync(movieJumanji);
            await dbContext.Movies.AddAsync(movieTheWayBack);
            await dbContext.Movies.AddAsync(movieBadBoysForLife);
            await dbContext.Movies.AddAsync(movieEmma);
            await dbContext.Movies.AddAsync(movieTheInvisibleMan);
            await dbContext.Movies.AddAsync(movieTheSpenserConfidential);
            await dbContext.Movies.AddAsync(movieTheGentlemen);
            await dbContext.Movies.AddAsync(movieBloodshot);
            await dbContext.Movies.AddAsync(movieNeverRarelySometimesAlways);
            await dbContext.Movies.AddAsync(movieTheClimb);
            await dbContext.Movies.AddAsync(movieDeerskin);
            await dbContext.Movies.AddAsync(movieMulan);
            await dbContext.Movies.AddAsync(movieImpracticalJokersTheMovie);
            await dbContext.Movies.AddAsync(movieTheHunt);
            await dbContext.Movies.AddAsync(movieIStillBelieve);
            await dbContext.Movies.AddAsync(movieInsidetheRaine);
        }
    }
}
