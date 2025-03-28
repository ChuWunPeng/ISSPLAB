﻿using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;

namespace OWB.SnipDemo.SDK
{
    public class Constants
    {
        #region DefaultPalettes
        public static Color[] Grey = new Color[]
        {
            Color.FromArgb(0,0,0),
            Color.FromArgb(1,1,1),
            Color.FromArgb(2,2,2),
            Color.FromArgb(3,3,3),
            Color.FromArgb(4,4,4),
            Color.FromArgb(5,5,5),
            Color.FromArgb(6,6,6),
            Color.FromArgb(7,7,7),
            Color.FromArgb(8,8,8),
            Color.FromArgb(9,9,9),
            Color.FromArgb(10,10,10),
            Color.FromArgb(11,11,11),
            Color.FromArgb(12,12,12),
            Color.FromArgb(13,13,13),
            Color.FromArgb(14,14,14),
            Color.FromArgb(15,15,15),
            Color.FromArgb(16,16,16),
            Color.FromArgb(17,17,17),
            Color.FromArgb(18,18,18),
            Color.FromArgb(19,19,19),
            Color.FromArgb(20,20,20),
            Color.FromArgb(21,21,21),
            Color.FromArgb(22,22,22),
            Color.FromArgb(23,23,23),
            Color.FromArgb(24,24,24),
            Color.FromArgb(25,25,25),
            Color.FromArgb(26,26,26),
            Color.FromArgb(27,27,27),
            Color.FromArgb(28,28,28),
            Color.FromArgb(29,29,29),
            Color.FromArgb(30,30,30),
            Color.FromArgb(31,31,31),
            Color.FromArgb(32,32,32),
            Color.FromArgb(33,33,33),
            Color.FromArgb(34,34,34),
            Color.FromArgb(35,35,35),
            Color.FromArgb(36,36,36),
            Color.FromArgb(37,37,37),
            Color.FromArgb(38,38,38),
            Color.FromArgb(39,39,39),
            Color.FromArgb(40,40,40),
            Color.FromArgb(41,41,41),
            Color.FromArgb(42,42,42),
            Color.FromArgb(43,43,43),
            Color.FromArgb(44,44,44),
            Color.FromArgb(45,45,45),
            Color.FromArgb(46,46,46),
            Color.FromArgb(47,47,47),
            Color.FromArgb(48,48,48),
            Color.FromArgb(49,49,49),
            Color.FromArgb(50,50,50),
            Color.FromArgb(51,51,51),
            Color.FromArgb(52,52,52),
            Color.FromArgb(53,53,53),
            Color.FromArgb(54,54,54),
            Color.FromArgb(55,55,55),
            Color.FromArgb(56,56,56),
            Color.FromArgb(57,57,57),
            Color.FromArgb(58,58,58),
            Color.FromArgb(59,59,59),
            Color.FromArgb(60,60,60),
            Color.FromArgb(61,61,61),
            Color.FromArgb(62,62,62),
            Color.FromArgb(63,63,63),
            Color.FromArgb(64,64,64),
            Color.FromArgb(65,65,65),
            Color.FromArgb(66,66,66),
            Color.FromArgb(67,67,67),
            Color.FromArgb(68,68,68),
            Color.FromArgb(69,69,69),
            Color.FromArgb(70,70,70),
            Color.FromArgb(71,71,71),
            Color.FromArgb(72,72,72),
            Color.FromArgb(73,73,73),
            Color.FromArgb(74,74,74),
            Color.FromArgb(75,75,75),
            Color.FromArgb(76,76,76),
            Color.FromArgb(77,77,77),
            Color.FromArgb(78,78,78),
            Color.FromArgb(79,79,79),
            Color.FromArgb(80,80,80),
            Color.FromArgb(81,81,81),
            Color.FromArgb(82,82,82),
            Color.FromArgb(83,83,83),
            Color.FromArgb(84,84,84),
            Color.FromArgb(85,85,85),
            Color.FromArgb(86,86,86),
            Color.FromArgb(87,87,87),
            Color.FromArgb(88,88,88),
            Color.FromArgb(89,89,89),
            Color.FromArgb(90,90,90),
            Color.FromArgb(91,91,91),
            Color.FromArgb(92,92,92),
            Color.FromArgb(93,93,93),
            Color.FromArgb(94,94,94),
            Color.FromArgb(95,95,95),
            Color.FromArgb(96,96,96),
            Color.FromArgb(97,97,97),
            Color.FromArgb(98,98,98),
            Color.FromArgb(99,99,99),
            Color.FromArgb(100,100,100),
            Color.FromArgb(101,101,101),
            Color.FromArgb(102,102,102),
            Color.FromArgb(103,103,103),
            Color.FromArgb(104,104,104),
            Color.FromArgb(105,105,105),
            Color.FromArgb(106,106,106),
            Color.FromArgb(107,107,107),
            Color.FromArgb(108,108,108),
            Color.FromArgb(109,109,109),
            Color.FromArgb(110,110,110),
            Color.FromArgb(111,111,111),
            Color.FromArgb(112,112,112),
            Color.FromArgb(113,113,113),
            Color.FromArgb(114,114,114),
            Color.FromArgb(115,115,115),
            Color.FromArgb(116,116,116),
            Color.FromArgb(117,117,117),
            Color.FromArgb(118,118,118),
            Color.FromArgb(119,119,119),
            Color.FromArgb(120,120,120),
            Color.FromArgb(121,121,121),
            Color.FromArgb(122,122,122),
            Color.FromArgb(123,123,123),
            Color.FromArgb(124,124,124),
            Color.FromArgb(125,125,125),
            Color.FromArgb(126,126,126),
            Color.FromArgb(127,127,127),
            Color.FromArgb(128,128,128),
            Color.FromArgb(129,129,129),
            Color.FromArgb(130,130,130),
            Color.FromArgb(131,131,131),
            Color.FromArgb(132,132,132),
            Color.FromArgb(133,133,133),
            Color.FromArgb(134,134,134),
            Color.FromArgb(135,135,135),
            Color.FromArgb(136,136,136),
            Color.FromArgb(137,137,137),
            Color.FromArgb(138,138,138),
            Color.FromArgb(139,139,139),
            Color.FromArgb(140,140,140),
            Color.FromArgb(141,141,141),
            Color.FromArgb(142,142,142),
            Color.FromArgb(143,143,143),
            Color.FromArgb(144,144,144),
            Color.FromArgb(145,145,145),
            Color.FromArgb(146,146,146),
            Color.FromArgb(147,147,147),
            Color.FromArgb(148,148,148),
            Color.FromArgb(149,149,149),
            Color.FromArgb(150,150,150),
            Color.FromArgb(151,151,151),
            Color.FromArgb(152,152,152),
            Color.FromArgb(153,153,153),
            Color.FromArgb(154,154,154),
            Color.FromArgb(155,155,155),
            Color.FromArgb(156,156,156),
            Color.FromArgb(157,157,157),
            Color.FromArgb(158,158,158),
            Color.FromArgb(159,159,159),
            Color.FromArgb(160,160,160),
            Color.FromArgb(161,161,161),
            Color.FromArgb(162,162,162),
            Color.FromArgb(163,163,163),
            Color.FromArgb(164,164,164),
            Color.FromArgb(165,165,165),
            Color.FromArgb(166,166,166),
            Color.FromArgb(167,167,167),
            Color.FromArgb(168,168,168),
            Color.FromArgb(169,169,169),
            Color.FromArgb(170,170,170),
            Color.FromArgb(171,171,171),
            Color.FromArgb(172,172,172),
            Color.FromArgb(173,173,173),
            Color.FromArgb(174,174,174),
            Color.FromArgb(175,175,175),
            Color.FromArgb(176,176,176),
            Color.FromArgb(177,177,177),
            Color.FromArgb(178,178,178),
            Color.FromArgb(179,179,179),
            Color.FromArgb(180,180,180),
            Color.FromArgb(181,181,181),
            Color.FromArgb(182,182,182),
            Color.FromArgb(183,183,183),
            Color.FromArgb(184,184,184),
            Color.FromArgb(185,185,185),
            Color.FromArgb(186,186,186),
            Color.FromArgb(187,187,187),
            Color.FromArgb(188,188,188),
            Color.FromArgb(189,189,189),
            Color.FromArgb(190,190,190),
            Color.FromArgb(191,191,191),
            Color.FromArgb(192,192,192),
            Color.FromArgb(193,193,193),
            Color.FromArgb(194,194,194),
            Color.FromArgb(195,195,195),
            Color.FromArgb(196,196,196),
            Color.FromArgb(197,197,197),
            Color.FromArgb(198,198,198),
            Color.FromArgb(199,199,199),
            Color.FromArgb(200,200,200),
            Color.FromArgb(201,201,201),
            Color.FromArgb(202,202,202),
            Color.FromArgb(203,203,203),
            Color.FromArgb(204,204,204),
            Color.FromArgb(205,205,205),
            Color.FromArgb(206,206,206),
            Color.FromArgb(207,207,207),
            Color.FromArgb(208,208,208),
            Color.FromArgb(209,209,209),
            Color.FromArgb(210,210,210),
            Color.FromArgb(211,211,211),
            Color.FromArgb(212,212,212),
            Color.FromArgb(213,213,213),
            Color.FromArgb(214,214,214),
            Color.FromArgb(215,215,215),
            Color.FromArgb(216,216,216),
            Color.FromArgb(217,217,217),
            Color.FromArgb(218,218,218),
            Color.FromArgb(219,219,219),
            Color.FromArgb(220,220,220),
            Color.FromArgb(221,221,221),
            Color.FromArgb(222,222,222),
            Color.FromArgb(223,223,223),
            Color.FromArgb(224,224,224),
            Color.FromArgb(225,225,225),
            Color.FromArgb(226,226,226),
            Color.FromArgb(227,227,227),
            Color.FromArgb(228,228,228),
            Color.FromArgb(229,229,229),
            Color.FromArgb(230,230,230),
            Color.FromArgb(231,231,231),
            Color.FromArgb(232,232,232),
            Color.FromArgb(233,233,233),
            Color.FromArgb(234,234,234),
            Color.FromArgb(235,235,235),
            Color.FromArgb(236,236,236),
            Color.FromArgb(237,237,237),
            Color.FromArgb(238,238,238),
            Color.FromArgb(239,239,239),
            Color.FromArgb(240,240,240),
            Color.FromArgb(241,241,241),
            Color.FromArgb(242,242,242),
            Color.FromArgb(243,243,243),
            Color.FromArgb(244,244,244),
            Color.FromArgb(245,245,245),
            Color.FromArgb(246,246,246),
            Color.FromArgb(247,247,247),
            Color.FromArgb(248,248,248),
            Color.FromArgb(249,249,249),
            Color.FromArgb(250,250,250),
            Color.FromArgb(251,251,251),
            Color.FromArgb(252,252,252),
            Color.FromArgb(253,253,253),
            Color.FromArgb(254,254,254),
            Color.FromArgb(255,255,255),
        };

        public static Color[] Iron = new Color[]
        {
            Color.FromArgb(0, 15, 20),
            Color.FromArgb(0, 14, 25),
            Color.FromArgb(0, 12, 31),
            Color.FromArgb(0, 11, 37),
            Color.FromArgb(0, 10, 43),
            Color.FromArgb(0, 9, 50),
            Color.FromArgb(0, 8, 56),
            Color.FromArgb(0, 7, 60),
            Color.FromArgb(0, 6, 66),
            Color.FromArgb(0, 5, 72),
            Color.FromArgb(0, 3, 75),
            Color.FromArgb(0, 3, 82),
            Color.FromArgb(2, 2, 85),
            Color.FromArgb(8, 0, 88),
            Color.FromArgb(10, 0, 95),
            Color.FromArgb(13, 0, 97),
            Color.FromArgb(18, 0, 102),
            Color.FromArgb(23, 0, 106),
            Color.FromArgb(25, 0, 109),
            Color.FromArgb(29, 0, 112),
            Color.FromArgb(33, 0, 119),
            Color.FromArgb(36, 0, 120),
            Color.FromArgb(40, 0, 123),
            Color.FromArgb(43, 0, 126),
            Color.FromArgb(47, 0, 130),
            Color.FromArgb(51, 0, 131),
            Color.FromArgb(54, 0, 134),
            Color.FromArgb(57, 0, 137),
            Color.FromArgb(60, 0, 138),
            Color.FromArgb(64, 0, 140),
            Color.FromArgb(67, 0, 143),
            Color.FromArgb(69, 0, 144),
            Color.FromArgb(74, 0, 145),
            Color.FromArgb(77, 0, 148),
            Color.FromArgb(79, 0, 149),
            Color.FromArgb(82, 0, 150),
            Color.FromArgb(86, 0, 151),
            Color.FromArgb(89, 0, 152),
            Color.FromArgb(92, 0, 155),
            Color.FromArgb(94, 0, 155),
            Color.FromArgb(96, 0, 156),
            Color.FromArgb(101, 0, 157),
            Color.FromArgb(103, 0, 159),
            Color.FromArgb(109, 0, 161),
            Color.FromArgb(113, 0, 160),
            Color.FromArgb(115, 0, 160),
            Color.FromArgb(118, 0, 161),
            Color.FromArgb(120, 0, 160),
            Color.FromArgb(123, 0, 162),
            Color.FromArgb(126, 0, 162),
            Color.FromArgb(128, 0, 161),
            Color.FromArgb(131, 0, 162),
            Color.FromArgb(132, 0, 160),
            Color.FromArgb(134, 0, 161),
            Color.FromArgb(138, 0, 162),
            Color.FromArgb(141, 0, 159),
            Color.FromArgb(144, 1, 160),
            Color.FromArgb(145, 2, 160),
            Color.FromArgb(149, 2, 159),
            Color.FromArgb(149, 5, 158),
            Color.FromArgb(150, 6, 155),
            Color.FromArgb(155, 6, 154),
            Color.FromArgb(156, 7, 153),
            Color.FromArgb(157, 8, 154),
            Color.FromArgb(161, 8, 154),
            Color.FromArgb(163, 10, 153),
            Color.FromArgb(164, 12, 150),
            Color.FromArgb(166, 13, 148),
            Color.FromArgb(169, 13, 149),
            Color.FromArgb(172, 14, 146),
            Color.FromArgb(172, 16, 147),
            Color.FromArgb(174, 17, 145),
            Color.FromArgb(177, 18, 144),
            Color.FromArgb(180, 19, 143),
            Color.FromArgb(179, 22, 138),
            Color.FromArgb(182, 23, 137),
            Color.FromArgb(183, 24, 134),
            Color.FromArgb(186, 24, 135),
            Color.FromArgb(189, 25, 133),
            Color.FromArgb(192, 26, 132),
            Color.FromArgb(193, 28, 129),
            Color.FromArgb(192, 31, 127),
            Color.FromArgb(197, 31, 124),
            Color.FromArgb(198, 33, 123),
            Color.FromArgb(198, 33, 119),
            Color.FromArgb(201, 35, 116),
            Color.FromArgb(200, 37, 117),
            Color.FromArgb(201, 38, 115),
            Color.FromArgb(206, 39, 112),
            Color.FromArgb(207, 40, 111),
            Color.FromArgb(208, 42, 108),
            Color.FromArgb(208, 45, 105),
            Color.FromArgb(210, 46, 102),
            Color.FromArgb(212, 48, 101),
            Color.FromArgb(213, 50, 99),
            Color.FromArgb(215, 51, 96),
            Color.FromArgb(215, 54, 93),
            Color.FromArgb(218, 54, 92),
            Color.FromArgb(219, 56, 89),
            Color.FromArgb(219, 59, 86),
            Color.FromArgb(220, 61, 85),
            Color.FromArgb(222, 62, 82),
            Color.FromArgb(222, 64, 80),
            Color.FromArgb(223, 66, 77),
            Color.FromArgb(226, 67, 76),
            Color.FromArgb(226, 70, 73),
            Color.FromArgb(227, 72, 70),
            Color.FromArgb(229, 73, 65),
            Color.FromArgb(229, 75, 66),
            Color.FromArgb(230, 77, 64),
            Color.FromArgb(231, 79, 61),
            Color.FromArgb(233, 81, 60),
            Color.FromArgb(234, 83, 57),
            Color.FromArgb(236, 84, 52),
            Color.FromArgb(238, 86, 49),
            Color.FromArgb(237, 89, 48),
            Color.FromArgb(237, 91, 47),
            Color.FromArgb(239, 92, 45),
            Color.FromArgb(239, 95, 42),
            Color.FromArgb(240, 97, 39),
            Color.FromArgb(241, 99, 36),
            Color.FromArgb(243, 100, 35),
            Color.FromArgb(244, 102, 32),
            Color.FromArgb(243, 105, 31),
            Color.FromArgb(244, 106, 31),
            Color.FromArgb(246, 108, 28),
            Color.FromArgb(245, 111, 25),
            Color.FromArgb(246, 113, 23),
            Color.FromArgb(248, 114, 22),
            Color.FromArgb(249, 116, 19),
            Color.FromArgb(248, 118, 20),
            Color.FromArgb(249, 120, 17),
            Color.FromArgb(249, 123, 17),
            Color.FromArgb(250, 125, 14),
            Color.FromArgb(250, 127, 13),
            Color.FromArgb(248, 127, 11),
            Color.FromArgb(249, 131, 9),
            Color.FromArgb(249, 131, 7),
            Color.FromArgb(248, 134, 6),
            Color.FromArgb(250, 135, 5),
            Color.FromArgb(250, 136, 1),
            Color.FromArgb(249, 138, 1),
            Color.FromArgb(249, 141, 0),
            Color.FromArgb(250, 142, 0),
            Color.FromArgb(249, 145, 0),
            Color.FromArgb(249, 147, 0),
            Color.FromArgb(250, 148, 0),
            Color.FromArgb(250, 150, 0),
            Color.FromArgb(249, 152, 0),
            Color.FromArgb(249, 154, 0),
            Color.FromArgb(250, 156, 0),
            Color.FromArgb(250, 158, 0),
            Color.FromArgb(249, 160, 0),
            Color.FromArgb(250, 163, 0),
            Color.FromArgb(249, 166, 0),
            Color.FromArgb(249, 170, 0),
            Color.FromArgb(250, 171, 0),
            Color.FromArgb(248, 173, 0),
            Color.FromArgb(249, 174, 0),
            Color.FromArgb(249, 176, 0),
            Color.FromArgb(248, 178, 0),
            Color.FromArgb(248, 180, 0),
            Color.FromArgb(249, 182, 0),
            Color.FromArgb(249, 184, 0),
            Color.FromArgb(250, 186, 0),
            Color.FromArgb(250, 187, 0),
            Color.FromArgb(249, 189, 0),
            Color.FromArgb(250, 191, 0),
            Color.FromArgb(250, 193, 1),
            Color.FromArgb(249, 195, 2),
            Color.FromArgb(249, 197, 3),
            Color.FromArgb(249, 199, 4),
            Color.FromArgb(250, 199, 8),
            Color.FromArgb(249, 201, 9),
            Color.FromArgb(249, 204, 8),
            Color.FromArgb(248, 205, 13),
            Color.FromArgb(250, 206, 14),
            Color.FromArgb(249, 208, 17),
            Color.FromArgb(249, 209, 21),
            Color.FromArgb(248, 212, 20),
            Color.FromArgb(249, 212, 23),
            Color.FromArgb(249, 214, 27),
            Color.FromArgb(250, 217, 29),
            Color.FromArgb(249, 218, 33),
            Color.FromArgb(249, 220, 36),
            Color.FromArgb(250, 221, 39),
            Color.FromArgb(250, 222, 42),
            Color.FromArgb(249, 224, 45),
            Color.FromArgb(249, 225, 49),
            Color.FromArgb(250, 226, 54),
            Color.FromArgb(249, 227, 57),
            Color.FromArgb(249, 229, 62),
            Color.FromArgb(249, 230, 65),
            Color.FromArgb(248, 231, 70),
            Color.FromArgb(249, 232, 76),
            Color.FromArgb(249, 233, 81),
            Color.FromArgb(250, 233, 86),
            Color.FromArgb(248, 236, 89),
            Color.FromArgb(248, 236, 96),
            Color.FromArgb(247, 238, 102),
            Color.FromArgb(247, 239, 107),
            Color.FromArgb(248, 239, 112),
            Color.FromArgb(247, 240, 117),
            Color.FromArgb(247, 241, 125),
            Color.FromArgb(247, 242, 131),
            Color.FromArgb(245, 244, 136),
            Color.FromArgb(246, 244, 141),
            Color.FromArgb(244, 246, 149),
            Color.FromArgb(243, 246, 157),
            Color.FromArgb(244, 247, 160),
            Color.FromArgb(242, 248, 169),
            Color.FromArgb(240, 250, 176),
            Color.FromArgb(242, 250, 185),
            Color.FromArgb(241, 250, 193),
            Color.FromArgb(241, 251, 202),
            Color.FromArgb(239, 251, 208),
            Color.FromArgb(239, 251, 217),
            Color.FromArgb(240, 251, 224),
            Color.FromArgb(238, 250, 235),
            Color.FromArgb(238, 251, 242),
            Color.FromArgb(236, 250, 248),
        };

        public static Color[] Rainbow = new Color[]
        {
            Color.FromArgb(0, 0, 0),
            Color.FromArgb(1, 0, 0),
            Color.FromArgb(5, 0, 6),
            Color.FromArgb(15, 0, 12),
            Color.FromArgb(25, 0, 23),
            Color.FromArgb(31, 0, 30),
            Color.FromArgb(42, 0, 42),
            Color.FromArgb(49, 0, 45),
            Color.FromArgb(57, 0, 56),
            Color.FromArgb(68, 0, 63),
            Color.FromArgb(72, 0, 75),
            Color.FromArgb(83, 0, 81),
            Color.FromArgb(90, 0, 92),
            Color.FromArgb(100, 0, 96),
            Color.FromArgb(109, 0, 109),
            Color.FromArgb(119, 0, 118),
            Color.FromArgb(126, 0, 126),
            Color.FromArgb(135, 0, 134),
            Color.FromArgb(145, 0, 142),
            Color.FromArgb(153, 0, 149),
            Color.FromArgb(159, 0, 156),
            Color.FromArgb(169, 0, 167),
            Color.FromArgb(177, 0, 175),
            Color.FromArgb(186, 0, 186),
            Color.FromArgb(193, 0, 192),
            Color.FromArgb(202, 0, 198),
            Color.FromArgb(210, 0, 209),
            Color.FromArgb(221, 0, 218),
            Color.FromArgb(208, 0, 213),
            Color.FromArgb(204, 0, 210),
            Color.FromArgb(195, 0, 212),
            Color.FromArgb(184, 0, 203),
            Color.FromArgb(178, 0, 206),
            Color.FromArgb(169, 0, 200),
            Color.FromArgb(157, 0, 194),
            Color.FromArgb(153, 0, 197),
            Color.FromArgb(144, 0, 191),
            Color.FromArgb(134, 0, 190),
            Color.FromArgb(127, 0, 187),
            Color.FromArgb(117, 0, 180),
            Color.FromArgb(108, 0, 181),
            Color.FromArgb(100, 0, 174),
            Color.FromArgb(91, 0, 174),
            Color.FromArgb(83, 0, 170),
            Color.FromArgb(75, 0, 165),
            Color.FromArgb(66, 0, 164),
            Color.FromArgb(56, 0, 159),
            Color.FromArgb(50, 0, 162),
            Color.FromArgb(40, 0, 156),
            Color.FromArgb(33, 0, 151),
            Color.FromArgb(25, 0, 150),
            Color.FromArgb(17, 0, 147),
            Color.FromArgb(7, 0, 143),
            Color.FromArgb(0, 0, 141),
            Color.FromArgb(0, 0, 140),
            Color.FromArgb(0, 6, 145),
            Color.FromArgb(0, 14, 145),
            Color.FromArgb(0, 22, 146),
            Color.FromArgb(0, 28, 149),
            Color.FromArgb(0, 39, 154),
            Color.FromArgb(0, 45, 154),
            Color.FromArgb(0, 56, 157),
            Color.FromArgb(0, 64, 159),
            Color.FromArgb(0, 72, 167),
            Color.FromArgb(0, 82, 167),
            Color.FromArgb(0, 89, 170),
            Color.FromArgb(0, 97, 172),
            Color.FromArgb(0, 106, 174),
            Color.FromArgb(0, 115, 178),
            Color.FromArgb(0, 123, 181),
            Color.FromArgb(0, 131, 184),
            Color.FromArgb(0, 139, 185),
            Color.FromArgb(0, 148, 189),
            Color.FromArgb(0, 154, 193),
            Color.FromArgb(0, 163, 197),
            Color.FromArgb(0, 173, 197),
            Color.FromArgb(0, 179, 199),
            Color.FromArgb(0, 190, 203),
            Color.FromArgb(0, 197, 206),
            Color.FromArgb(0, 205, 207),
            Color.FromArgb(0, 214, 211),
            Color.FromArgb(0, 222, 213),
            Color.FromArgb(0, 217, 206),
            Color.FromArgb(0, 212, 200),
            Color.FromArgb(0, 205, 188),
            Color.FromArgb(0, 200, 181),
            Color.FromArgb(0, 196, 171),
            Color.FromArgb(0, 190, 163),
            Color.FromArgb(0, 185, 155),
            Color.FromArgb(0, 179, 147),
            Color.FromArgb(0, 173, 139),
            Color.FromArgb(0, 168, 131),
            Color.FromArgb(0, 162, 125),
            Color.FromArgb(0, 156, 115),
            Color.FromArgb(0, 149, 108),
            Color.FromArgb(0, 145, 95),
            Color.FromArgb(0, 142, 89),
            Color.FromArgb(0, 134, 81),
            Color.FromArgb(0, 129, 74),
            Color.FromArgb(0, 126, 64),
            Color.FromArgb(0, 119, 59),
            Color.FromArgb(0, 115, 48),
            Color.FromArgb(0, 109, 39),
            Color.FromArgb(0, 101, 34),
            Color.FromArgb(0, 97, 21),
            Color.FromArgb(0, 92, 15),
            Color.FromArgb(0, 84, 7),
            Color.FromArgb(0, 80, 0),
            Color.FromArgb(0, 73, 0),
            Color.FromArgb(0, 79, 0),
            Color.FromArgb(5, 84, 0),
            Color.FromArgb(14, 89, 0),
            Color.FromArgb(19, 96, 0),
            Color.FromArgb(28, 99, 0),
            Color.FromArgb(36, 107, 0),
            Color.FromArgb(45, 114, 0),
            Color.FromArgb(49, 116, 0),
            Color.FromArgb(60, 121, 0),
            Color.FromArgb(69, 126, 0),
            Color.FromArgb(74, 132, 0),
            Color.FromArgb(82, 136, 0),
            Color.FromArgb(94, 140, 0),
            Color.FromArgb(99, 148, 0),
            Color.FromArgb(105, 151, 0),
            Color.FromArgb(114, 157, 0),
            Color.FromArgb(123, 160, 0),
            Color.FromArgb(128, 167, 0),
            Color.FromArgb(134, 171, 0),
            Color.FromArgb(145, 178, 0),
            Color.FromArgb(153, 184, 0),
            Color.FromArgb(159, 188, 0),
            Color.FromArgb(168, 194, 0),
            Color.FromArgb(177, 197, 0),
            Color.FromArgb(184, 202, 0),
            Color.FromArgb(189, 208, 0),
            Color.FromArgb(202, 212, 0),
            Color.FromArgb(205, 219, 0),
            Color.FromArgb(215, 225, 0),
            Color.FromArgb(211, 217, 0),
            Color.FromArgb(207, 207, 0),
            Color.FromArgb(205, 202, 0),
            Color.FromArgb(200, 193, 0),
            Color.FromArgb(196, 183, 0),
            Color.FromArgb(192, 175, 0),
            Color.FromArgb(188, 169, 0),
            Color.FromArgb(187, 160, 0),
            Color.FromArgb(179, 152, 0),
            Color.FromArgb(179, 143, 0),
            Color.FromArgb(176, 137, 0),
            Color.FromArgb(169, 131, 0),
            Color.FromArgb(165, 122, 0),
            Color.FromArgb(163, 113, 0),
            Color.FromArgb(158, 107, 0),
            Color.FromArgb(156, 102, 0),
            Color.FromArgb(153, 94, 0),
            Color.FromArgb(148, 83, 0),
            Color.FromArgb(144, 75, 0),
            Color.FromArgb(142, 68, 0),
            Color.FromArgb(139, 60, 0),
            Color.FromArgb(134, 53, 0),
            Color.FromArgb(131, 42, 0),
            Color.FromArgb(130, 33, 0),
            Color.FromArgb(124, 27, 0),
            Color.FromArgb(118, 21, 0),
            Color.FromArgb(116, 12, 0),
            Color.FromArgb(113, 6, 0),
            Color.FromArgb(109, 0, 0),
            Color.FromArgb(112, 0, 0),
            Color.FromArgb(117, 0, 0),
            Color.FromArgb(119, 2, 0),
            Color.FromArgb(123, 4, 0),
            Color.FromArgb(126, 5, 0),
            Color.FromArgb(129, 6, 2),
            Color.FromArgb(136, 6, 5),
            Color.FromArgb(136, 9, 5),
            Color.FromArgb(143, 9, 9),
            Color.FromArgb(143, 14, 6),
            Color.FromArgb(145, 17, 6),
            Color.FromArgb(149, 17, 10),
            Color.FromArgb(153, 19, 11),
            Color.FromArgb(156, 21, 15),
            Color.FromArgb(158, 21, 14),
            Color.FromArgb(159, 23, 18),
            Color.FromArgb(165, 25, 17),
            Color.FromArgb(173, 23, 21),
            Color.FromArgb(171, 28, 23),
            Color.FromArgb(176, 29, 23),
            Color.FromArgb(180, 31, 25),
            Color.FromArgb(184, 32, 29),
            Color.FromArgb(185, 37, 24),
            Color.FromArgb(188, 37, 27),
            Color.FromArgb(193, 37, 33),
            Color.FromArgb(196, 40, 31),
            Color.FromArgb(200, 42, 32),
            Color.FromArgb(200, 45, 34),
            Color.FromArgb(203, 50, 39),
            Color.FromArgb(204, 57, 51),
            Color.FromArgb(206, 63, 55),
            Color.FromArgb(209, 70, 65),
            Color.FromArgb(208, 78, 68),
            Color.FromArgb(209, 86, 74),
            Color.FromArgb(211, 90, 88),
            Color.FromArgb(214, 98, 94),
            Color.FromArgb(217, 103, 99),
            Color.FromArgb(218, 112, 103),
            Color.FromArgb(217, 117, 115),
            Color.FromArgb(219, 126, 121),
            Color.FromArgb(222, 130, 126),
            Color.FromArgb(223, 138, 133),
            Color.FromArgb(222, 145, 139),
            Color.FromArgb(224, 151, 147),
            Color.FromArgb(227, 157, 151),
            Color.FromArgb(228, 165, 159),
            Color.FromArgb(227, 172, 165),
            Color.FromArgb(228, 179, 175),
            Color.FromArgb(232, 185, 176),
            Color.FromArgb(233, 193, 186),
            Color.FromArgb(231, 200, 192),
            Color.FromArgb(233, 208, 198),
            Color.FromArgb(235, 214, 205),
            Color.FromArgb(234, 223, 211),
            Color.FromArgb(236, 228, 219),
            Color.FromArgb(242, 233, 229),
            Color.FromArgb(242, 240, 234),
        };
        #endregion

        public static Dictionary<string, Color[]> Palettes = new Dictionary<string, Color[]>
        {
            { "grey", Grey },
            { "iron", Iron },
            { "rainbow", Rainbow }
        };

        public static string DefaultIRFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SnipIR");
    }
}
