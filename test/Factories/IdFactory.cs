/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

namespace Gestaoaju.Factories
{
    public static class IdFactory
    {
        private static int id = 0;
        public static int Id => ++id;
    }
}
