﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using kursach.Model;
using System.Windows;

namespace kursach
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static User currentUser = null;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //using (var context = new DatabaseContext())
            //{
            //    context.AddUser("Мария", "Морозова", "Moroz3307@mail.ru", "Moroz74!");
            //    context.AddUser("Дарья", "Тихонова", "Tixr4309@mail.ru", "Tixr741*");
            //    context.AddUser("Саша", "Казанцева", "zakatalki@gmil.com", "Kazan88+");
            //    context.AddUser("Диана", "Шепелёва", "Sheep90@gmail.com", "Shpil901^");
            //    context.AddUser("Анастасия", "Креслина", "Sofa8710@gmaol.com", "Kreslo0?");

            //    context.AddAuthor("Владимир", "Маяковский", "Владимир Маяковский (1893-1930) - русский поэт, публицист, драматург и художник, один из ярких представителей русского футуризма. Его творчество характеризуется смелыми экспериментами в поэтической форме, использованием новаторских языковых приемов и выраженной социальной направленностью.", 1);
            //    context.AddAuthor("Луиза Мэй", "Олкотт", "Луиза Мэй Олкотт (1832-1888) - американская писательница, писала стихи, рассказы и другие произведения, которые отличались глубоким пониманием человеческой натуры и сочувствием к героям своих произведений.", 1);
            //    context.AddAuthor("Дэниел", "Киз", "Дэниел Киз (1927-2018) - американский писатель и преподаватель, известный своими философскими и научно-фантастическими произведениями. Его творчество отличается глубокими философскими размышлениями о месте человека во Вселенной и будущем человечества.", 1);
            //    context.AddAuthor("Эрих Мария", "Ремарк", "Эрих Мария Ремарк (1898-1970) - немецкий писатель, известен своими произведениями, в которых он затрагивает темы войны, человеческой судьбы и морали. Его работы часто вызывают размышления о бессмысленности войны и страданиях людей, попавших в ее когти.", 1);
            //    context.AddAuthor("Вирджиния", "Вулф", "Вирджииния Вулф — английская писательница, литературная критикесса, феминистка. Ведущая фигура модернистской литературы первой половины XX века. В межвоенный период Вулф была значительной фигурой в лондонском литературном обществе и членом группы Блумсбери.", 1);
            //    context.AddAuthor("Марина", "Цветаева", "Марина Цветаева (1892-1941) - выдающаяся русская поэтесса, одна из самых ярких фигур русской литературы XX века. Ее стихи отличаются глубоким лиризмом, интенсивными эмоциями и остротой мысли. Ее творчество оказало значительное влияние на развитие русской поэзии.", 1);

            //    context.AddBook("Во весь голос (сборник)", 1, "D:\\3_курс\\рпм\\курсовой\\книги\\Владимир_Маяковский_Во_весь_голос_сборник.fb2", "D:\\3_курс\\рпм\\курсовой\\книги\\Во_Весь_Голос_Обложка", 252, null, 2016, "9785170992690", "7 ч.", "В этот сборник вошли наиболее известные поэмы и стихотворения Маяковского разных лет, в полной мере представляющие читателю уникальный стиль поэта: «Флейта-позвоночник», «Облако в штанах», «Про это», «А вы могли бы?», «Любовь», «Во весь голос», стихи из «американского» цикла и др. Оригинальность, бунтарство, резкость стихотворных строк и невероятная, берущая за душу эмоциональность Маяковского, поражавшие его современников, и сейчас не оставят читателя равнодушным.", "12+", 1, 1);
            //    context.AddBook("Маленькие женщины", 2, "D:\\3_курс\\рпм\\курсовой\\книги\\Луиза_Мэй_Олкотт_Маленькие_женщины.fb2", "D:\\3_курс\\рпм\\курсовой\\книги\\Маленькие_Женщины_Обложка", 327, 1868, 2020, "9785389172258", "9 ч.", "Маленькие женщины» Луизы Мэй Олкотт – это роман, на котором воспитывалось не одно поколение читателей по всему миру. Впервые опубликованное в 1868 году в США, это произведение было переведено более чем на 50 языков и положено в основу шести фильмов, четырех телесериалов, нескольких опер и спектаклей. История семейства Марч, в котором подрастают четыре дружные, но непохожие друг на друга сестры, заключает в себе узнаваемые перипетии юности, взросления, дружбы и любви.", "12+", 0, 1);
            //    context.AddBook("Цветы для Элджернона", 3, "D:\\3_курс\\рпм\\курсовой\\книги\\Дэниел_Киз_Цветы_для_Элджернона.fb2", "D:\\3_курс\\рпм\\курсовой\\книги\\Цветы_Для_Элджернона_Обложка", 213, 1966, 2014, "9785699556991", "6 ч.", "Когда-то это считалось фантастикой. Сейчас это воспринимается как одно из самых человечных произведений новейшего времени, как роман пронзительной психологической силы, как филигранное развитие темы любви и ответственности. Изменения с Чарли Гордоном происходят на наших глазах, здесь и сейчас.В первых отчетах полно орфографических ошибок, ему очень трудно излагать свои мысли, постепенно он начинает писать правильно и рассказывать о событиях идеально. Гении и идиоты – две стороны одной медали.У них разные IQ, отличаются условия жизни, но они похожи в одном – часто и те, и другие бесконечно одиноки.А цена за мечту – стать умным – становится слишком высокой. У Чарли есть двойник – мышонок Элджернон, только Чарли понимает поведение мышонка и то, что ему самому отведена та же роль подопытной мыши,не больше. Никого не оставляет равнодушным.", "12+", 0, 1);
            //    context.AddBook("Триумфальная арка", 4, "D:\\3_курс\\рпм\\курсовой\\книги\\Эрих_Мария_Ремарк_Триумфальная_арка.fb2", "D:\\3_курс\\рпм\\курсовой\\книги\\Триумфальная_Арка_Обложка", 540, 1945, 2014, "9785170871933", "13 ч.", "Триумфальная арка - пронзительная история любви всему наперекор, любви, приносящей боль, но и дарующей бесконечную радость. Место действия - Париж накануне Второй мировой войны. Герой - беженец из Германии, без документов, скрывающийся и от французов,и от нацистов, хирург, спасающий человеческие жизни. Героиня - итальянская актриса, окруженная поклонниками, вспыльчивая, как все артисты, прекрасная и неотразимая. И время, когда влюбленным довелось встретиться, и город, пронизанный ощущением надвигающейся катастрофы, становятся героями этого романа.", "16+", 1, 1);
            //    context.AddBook("Орландо. Волны. Флаш (сборник)", 5, "D:\\3_курс\\рпм\\курсовой\\книги\\Вирджиния_Вулф_Орландо_Волны_Флаш.fb2", "D:\\3_курс\\рпм\\курсовой\\книги\\Орландо_Волны_Флаш_Обложка", 549, null, 2018, "9785171098155", "14 ч.", "Произведения, вошедшие в этот сборник, во всей красе демонстрируют многогранность таланта Вирджинии Вулф – писательницы, умевшей быть и мудрым философом, и поэтичным фантазером, и изысканным модернистом, и блестящей шутницей.", "16+", 1, 1);
            //    context.AddBook("Повесть о Сонечке", 6, "D:\\3_курс\\рпм\\курсовой\\книги\\Марина_Цветаева_Повесть_о_Сонечке.fb2", "D:\\3_курс\\рпм\\курсовой\\книги\\Повесть_О_Сонечке_Обложка", 144, 1937, 2001, "5040083971", "≈ 4 ч.", "Повесть посвящена памяти актрисы и чтицы Софьи Евгеньевны Голлидэй (1894—1934), с которой Цветаева была дружна с конца 1918 по весну 1919 года. Тогда же она посвятила ей цикл стихотворений, написала для неё роли в пьесах «Фортуна», «Приключение», «каменный Ангел», «Феникс».", "12+", 0, 1);

            //    context.AddCategory("Бизнес-книги");
            //    context.AddCategory("Зарубежная литература");
            //    context.AddCategory("Фантастика");
            //    context.AddCategory("Книги по психологии");
            //    context.AddCategory("Классическая литература");

            //    context.AddBook_Category(1, 5);
            //    context.AddBook_Category(2, 2);
            //    context.AddBook_Category(3, 3);
            //    context.AddBook_Category(4, 2);
            //    context.AddBook_Category(4, 5);
            //    context.AddBook_Category(5, 2);
            //    context.AddBook_Category(5, 5);
            //    context.AddBook_Category(6, 5);

            //    context.AddGenre(1, "Банковское дело");
            //    context.AddGenre(2, "Зарубежная классика");
            //    context.AddGenre(3, "Научная фантастика");
            //    context.AddGenre(4, "Психология управления");
            //    context.AddGenre(5, "Литература 20 века");

            //    context.AddTheme("Гражданская позиция");
            //    context.AddTheme("Становление героя");
            //    context.AddTheme("Американская литература");
            //    context.AddTheme("Истории о любви");
            //    context.AddTheme("Проза жизни");
            //    context.AddTheme("Повести");
            //    context.AddTheme("Экранизация");//Маленьки женщины, Цветы для Элджернона

            //    context.AddBookshelf(2, "Любимые книги", "#F08080");
            //    context.AddBookshelf(3, "Прочитать позже", "#FFD700");
            //    context.AddBookshelf(4, "Самое любимое", "#9370DB");
            //    context.AddBookshelf(4, "На будущее", "#FFF8DC");

            //    context.AddBook_Bookshelf(1, 1);
            //    context.AddBook_Bookshelf(5, 1);
            //    context.AddBook_Bookshelf(6, 2);
            //    context.AddBook_Bookshelf(3, 3);
            //    context.AddBook_Bookshelf(2, 4);
            //    context.AddBook_Bookshelf(5, 4);

            //    context.AddBook_Theme(1, 1);
            //    context.AddBook_Theme(2, 2);
            //    context.AddBook_Theme(2, 7);
            //    context.AddBook_Theme(3, 3);
            //    context.AddBook_Theme(3, 7);
            //    context.AddBook_Theme(4, 4);
            //    context.AddBook_Theme(5, 5);
            //    context.AddBook_Theme(6, 6);
            //}
        }
    }
}
