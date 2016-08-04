using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIOCaseRU
{
     class Rules
    {
       // [YamlAlias("lastname")]
        public RuleSet LastName { get; set; }

        //[YamlAlias("firstname")]
        public RuleSet FirstName { get; set; }

       // [YamlAlias("middlename")]
        public RuleSet MiddleName { get; set; }
    }
     class Rule
    {
       // [YamlAlias("gender")]
        public string Gender { get; set; }

       // [YamlAlias("test")]
        public List<string> TestSuffixes { get; set; }

       // [YamlAlias("mods")]
        public List<string> ModSuffixes { get; set; }

       // [YamlAlias("tags")]
        public List<string> Tags { get; set; }
    }
     class RuleSet
    {
       // [YamlAlias("exceptions")]
        public List<Rule> Exceptions { get; set; }

       // [YamlAlias("suffixes")]
        public List<Rule> Suffixes { get; set; }
    }
    class RulesFile
    {
        private const string rulesXML = @"
<!-- Набор правил для склонения русских имён, фамилий и отчеств по падежам.
 = Описание формата файла
 Файл содержит 3 группы правил. Это +lastname+, +firstname+ и +middlename+. Каждая группа
 содержит подгруппы с правилами:
 * Подгруппа +exceptions+ (её может не быть)
 * Подгруппа +suffixes+
 == Правила
 В свою очередь, каждая подгруппа содержит набор правил. Каждое правило содержит 3 составляющие:
 * Пол (gender). Допустимые значения: +male+, +female+, +androgynous+
 * Что заменять (test). Массив суффиксов для замены.
 * На что заменять (mods). Массив модификаторов. Модификатор может иметь впереди один или 
    дефис, он означает количество символов, которые нужно будет вырезать из слова.
 Правила отделяются друг от друга переносом строки для лучшего восприятия.
 === Из чего состоят суффиксы
 === Из чего состоят модификаторы
   родительный, дательный, винительный, творительный, предложный
 == Как добавить новое правило
-->
<lastname>
  <!-- Неизменяемые первые части двойных русских фамилий.-->
  <exceptions>
    <suffixes>
      <suffix>
        <gender>androgynous</gender>
        <test>
          бонч,
          абдул,
          белиц,
          гасан,
          дюссар,
          дюмон,
          книппер,
          корвин,
          ван,
          шолом,
          тер,
          призван,
          мелик,
          вар
        </test>
        <mods>., ., ., ., .</mods>
      </suffix>
    </suffixes>
    <tags>first_word</tags>
    <suffixes>
      <suffix>
        <gender>androgynous</gender>
        <test>дюма, тома, дега, люка, ферма, гамарра, петипа, шандра, скаля</test>
        <mods>., ., ., ., .</mods>
      </suffix>
      <suffix>
        <gender>androgynous</gender>
        <test>гусь, ремень, камень, онук, богода, нечипас, долгопалец, маненок, рева, кива</test>
        <mods>., ., ., ., .</mods>
      </suffix>
      <suffix>
        <gender>androgynous</gender>
        <test>вий, сой, цой, хой</test>
        <mods>-я, -ю, -я, -ем, -е</mods>
      </suffix>
      <suffix>
        <gender>androgynous</gender>
        <test>я</test>
        <mods>., ., ., ., .</mods>
      </suffix>
    </suffixes>
  </exceptions>
  <suffixes>
    <suffix>
      <gender>female</gender>
      <test>б, в, г, д, ж, з, й, к, л, м, н, п, р, с, т, ф, х, ц, ч, ш, щ, ъ, ь</test>
      <mods>., ., ., ., .</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>гава, орота</test>
      <mods>., ., ., ., .</mods>
    </suffix>
    <suffix>
      <gender>female</gender>
      <test>ска, цка</test>
      <mods>-ой, -ой, -ую, -ой, -ой</mods>
    </suffix>
    <suffix>
      <gender>female</gender>
      <test>ая</test>
      <mods>--ой, --ой, --ую, --ой, --ой</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ская</test>
      <mods>--ой, --ой, --ую, --ой, --ой</mods>
    </suffix>
    <suffix>
      <gender>female</gender>
      <test>на</test>
      <mods>-ой, -ой, -у, -ой, -ой</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>иной</test>
      <mods>-я, -ю, -я, -ем, -е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>уй</test>
      <mods>-я, -ю, -я, -ем, -е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ца</test>
      <mods>-ы, -е, -у, -ей, -е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>рих</test>
      <mods>а, у, а, ом, е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ия</test>
      <mods>., ., ., ., .</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>иа, аа, оа, уа, ыа, еа, юа, эа</test>
      <mods>., ., ., ., .</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>их, ых</test>
      <mods>., ., ., ., .</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>о, е, э, и, ы, у, ю</test>
      <mods>., ., ., ., .</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ова, ева</test>
      <mods>-ой, -ой, -у, -ой, -ой</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>га, ка, ха, ча, ща, жа</test>
      <mods>-и, -е, -у, -ой, -е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ца</test>
      <mods>-и, -е, -у, -ей, -е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>а</test>
      <mods>-ы, -е, -у, -ой, -е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ь</test>
      <mods>-я, -ю, -я, -ем, -е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ия</test>
      <mods>-и, -и, -ю, -ей, -и</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>я</test>
      <mods>-и, -е, -ю, -ей, -е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ей</test>
      <mods>-я, -ю, -я, -ем, -е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ян, ан, йн</test>
      <mods>а, у, а, ом, е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ынец, обец</test>
      <mods>--ца, --цу, --ца, --цем, --це</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>онец, овец</test>
      <mods>--ца, --цу, --ца, --цом, --це</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ай</test>
      <mods>-я, -ю, -я, -ем, -е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>гой, кой</test>
      <mods>-го, -му, -го, --им, -м</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ой</test>
      <mods>-го, -му, -го, --ым, -м</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ах, ив</test>
      <mods>а, у, а, ом, е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ший, щий, жий, ний</test>
      <mods>--его, --ему, --его, -м, --ем</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>кий, ый</test>
      <mods>--ого, --ому, --ого, -м, --ом</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ий</test>
      <mods>-я, -ю, -я, -ем, -и</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ок</test>
      <mods>--ка, --ку, --ка, --ком, --ке</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ец</test>
      <mods>--ца, --цу, --ца, --цом, --це</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ц, ч, ш, щ</test>
      <mods>а, у, а, ем, е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>в, н</test>
      <mods>а, у, а, ым, е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>б, г, д, ж, з, к, л, м, п, р, с, т, ф, х</test>
      <mods>а, у, а, ом, е</mods>
    </suffix>
  </suffixes>
</lastname>
<firstname>
  exceptions:
  <exceptions>
    <suffix>
      <gender>androgynous</gender>
      <test>лев</test>
      <mods>--ьва, --ьву, --ьва, --ьвом, --ьве</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>пётр</test>
      <mods>---етра, ---етру, ---етра, ---етром, ---етре</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>павел</test>
      <mods>--ла, --лу, --ла, --лом, --ле</mods>
    </suffix>
    <suffix>
      <gender>male</gender>
      <test>яша</test>
      <mods>-и, -е, -у, -ей, -е</mods>
    </suffix>
    <suffix>
      <gender>male</gender>
      <test>шота</test>
      <mods>., ., ., ., .</mods>
    </suffix>
    <suffix>
      <gender>female</gender>
      <test>рашель, нинель, николь, габриэль, даниэль</test>
      <mods>., ., ., ., .</mods>
    </suffix>
  </exceptions>
  <suffixes>
    <suffix>
      <gender>androgynous</gender>
      <test>е, ё, и, о, у, ы, э, ю</test>
      <mods>., ., ., ., .</mods>
    </suffix>
    <suffix>
      <gender>female</gender>
      <test>б, в, г, д, ж, з, й, к, л, м, н, п, р, с, т, ф, х, ц, ч, ш, щ, ъ</test>
      <mods>., ., ., ., .</mods>
    </suffix>
    <suffix>
      <gender>female</gender>
      <test>ь</test>
      <mods>-и, -и, ., ю, -и</mods>
    </suffix>
    <suffix>
      <gender>male</gender>
      <test>ь</test>
      <mods>-я, -ю, -я, -ем, -е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>га, ка, ха, ча, ща, жа</test>
      <mods>-и, -е, -у, -ой, -е</mods>
    </suffix>
    <suffix>
      <gender>female</gender>
      <test>ша</test>
      <mods>-и, -е, -у, -ей, -е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>а</test>
      <mods>-ы, -е, -у, -ой, -е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ия</test>
      <mods>-и, -и, -ю, -ей, -и</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>я</test>
      <mods>-и, -е, -ю, -ей, -е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ей</test>
      <mods>-я, -ю, -я, -ем, -е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>ий</test>
      <mods>-я, -ю, -я, -ем, -и</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>й</test>
      <mods>-я, -ю, -я, -ем, -е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>б, в, г, д, ж, з, к, л, м, н, п, р, с, т, ф, х, ц, ч</test>
      <mods>а, у, а, ом, е</mods>
    </suffix>
  </suffixes>
</firstname>
<middlename>
  <suffixes>
    <suffix>
      <gender>androgynous</gender>
      <test>ич</test>
      <mods>а, у, а, ем, е</mods>
    </suffix>
    <suffix>
      <gender>androgynous</gender>
      <test>на</test>
      <mods>-ы, -е, -у, -ой, -е</mods>
    </suffix>
  </suffixes>
</middlename>";
    }
}
