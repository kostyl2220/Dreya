﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    static class GameDefs
    {
        public static bool USE_HIGHLIGHT = true;

        public static int PLAYER_LAYER = LayerMask.NameToLayer("Player");
        public static int ENEMY_LAYER = LayerMask.NameToLayer("Enemy");

        public static int LIGHT_LAYER = LayerMask.NameToLayer("Light");
        public static string PLAYER_TAG = "Player";
    }
}
