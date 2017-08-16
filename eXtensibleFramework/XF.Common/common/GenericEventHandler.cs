// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{

    public delegate void GenericEventHandler();

    public delegate void GenericEventHandler<T>(T t);

    public delegate void GenericEventHandler<T, U>(T t, U u);

    public delegate void GenericEventHandler<T, U, V>(T t, U u, V v);

    public delegate void GenericEventHandler<T, U, V, W>(T t, U u, V v, W w);

    public delegate void GenericEventHandler<T, U, V, W, X>(T t, U u, V v, W w, X x);

    public delegate void GenericEventHandler<T, U, V, W, X, Y>(T t, U u, V v, W w, X x, Y y);

    public delegate void GenericEventHandler<T, U, V, W, X, Y, Z>(T t, U u, V v, W w, X x, Y y, Z z);
}

