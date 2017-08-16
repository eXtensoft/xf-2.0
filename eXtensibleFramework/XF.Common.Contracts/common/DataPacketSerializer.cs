// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common.Wcf
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DataPacketSerializer
    {

        public DataPacket Serialize<T>(IContext ctx, ModelActionOption modelAction, T model) where T : class, new()
        {
            DataPacket item = new DataPacket() 
            { 
                Context = (ApplicationContext)ctx, 
                ModelAction = modelAction,
                Typename = GetTypename<T>(),
                Items = GenerateMessage<T>(ctx, modelAction),
                Buffer = GenericSerializer.ItemToByteArray(model)
            };

            return item;
        }

        public DataPacket Serialize<T>(IContext ctx, ModelActionOption modelAction, IEnumerable<TypedItem> items) where T : class, new()
        {
            DataPacket item = new DataPacket()
            {
                Context = (ApplicationContext)ctx,
                Typename = GetTypename<T>(),
                ModelAction = modelAction,
                Criteria = new Criterion() { Items= items }, 
                Items = GenerateMessage<T>(ctx,modelAction)
            };

            return item;
        }

        public DataPacket Serialize<T>(IContext ctx, ModelActionOption modelAction, T model, IEnumerable<TypedItem> items) where T : class, new()
        {
            DataPacket item = new DataPacket()
            {
                Context = (ApplicationContext)ctx,
                Typename = GetTypename<T>(),
                ModelAction = modelAction,
                Criteria = new Criterion() { Items = items },
                Items = GenerateMessage<T>(ctx,modelAction),
                Buffer = GenericSerializer.ItemToByteArray(model)
            };
            return item;
        }

        public DataPacket Serialize<T>(IContext ctx, ModelActionOption modelAction, IEnumerable<T> list, IEnumerable<TypedItem> items) where T : class, new()
        {
            DataPacket item = new DataPacket()
            {
                Context = (ApplicationContext)ctx,
                Typename = GetTypename<List<T>>(),
                ModelAction = modelAction,
                Criteria = new Criterion() { Items = items },
                Items = GenerateMessage<T>(ctx, modelAction),
                Buffer = GenericSerializer.GenericListToByteArray<T>(list.ToList())
            };
            return item;           
        }

        public DataPacket Serialize<T,U>(IContext ctx, ModelActionOption modelAction, IEnumerable<T> list, IEnumerable<TypedItem> items) where T : class, new()
        {
            DataPacket item = new DataPacket()
            {
                Context = (ApplicationContext)ctx,
                Typename = GetTypename<T>(),
                ModelAction = modelAction,
                Criteria = new Criterion() { Items = items },
                Items = GenerateMessage<T>(ctx, modelAction),
                Buffer = GenericSerializer.GenericListToByteArray<T>(list.ToList()),
                SecondaryTypename = typeof(U).FullName
            };
            return item;
        }



        public DataPacket Serialize<T,U>(IContext ctx, ModelActionOption modelAction, T model, IEnumerable<TypedItem> items) where T : class, new()
        {
            DataPacket item = new DataPacket()
            {
                Context = (ApplicationContext)ctx,
                Typename = GetTypename<T>(),
                ModelAction = modelAction,
                Criteria = new Criterion() { Items = items },
                Items = GenerateMessage<T>(ctx, modelAction),
                Buffer = GenericSerializer.ItemToByteArray(model),
                SecondaryTypename = typeof(U).FullName
            };
            return item;
        }


        public IResponse<T> Deserialize<T>(DataPacket item) where T : class, new()
        {
            Message<T> data = null;
            if (item != null)
            {
                data = new Message<T>(item.Items);
                switch (item.ModelAction)
                {
                    case ModelActionOption.None:
                        break;
                    case ModelActionOption.Post:
                    case ModelActionOption.Put:
                    case ModelActionOption.Get:
                        if (item.Buffer != null)
                        {
                            data.Content = new List<T>();
                            data.Content.Add(GenericSerializer.ByteArrayToGenericItem<T>(item.Buffer));                        
                        }
                        break;
                    case ModelActionOption.Delete:
                        break;
                    case ModelActionOption.GetAll:
                        if (item.Buffer != null)
                        {
                            data.Content = GenericSerializer.ByteArrayToGenericItem<List<T>>(item.Buffer);
                        }
                        break;
                    case ModelActionOption.GetAllProjections:
                        if (item.Buffer != null)
                        {
                            //TODO  FIX THIS 
                            //data.Display = GenericSerializer.ByteArrayToGenericItem<List<Projection>>(item.Buffer);
                        }
                        break;
                    case ModelActionOption.ExecuteAction:
                        break;
                    case ModelActionOption.ExecuteCommand:
                        if (item.Tables != null)
                        {
                            data.Data = item.Tables;
                        }
                        break;
                    case ModelActionOption.ExecuteMany:
                        break;
                    default:
                        break;
                }                
            }
            else
            {
                data = new Message<T>(new List<TypedItem>());
            }
            if (item != null && !item.IsOkay)
            {
                data.SetError(500, item.ErrorMessage);
            }

            return data as IResponse<T>;
        }


        public IResponse<T,U> Deserialize<T,U>(DataPacket item) where T : class, new()
        {
            Message<T,U> data = null;
            if (item != null)
            {
                data = new Message<T,U>(item.Items);
                data.ActionResult = GenericSerializer.Deserialize<U>(item.Buffer);
            }
            return data as IResponse<T, U>;
        }

        private static string GetTypename<T>() where T : class, new()
        {
            Type type = GetModelType<T>();
            return type.AssemblyQualifiedName;
        }
 
        private static Type GetModelType<T>()
        {
            T t = Activator.CreateInstance<T>();
            return t.GetType();
        }

        private static Criterion GenerateCriterion(IEnumerable<TypedItem> items)
        {
            return new Criterion() { Items = items };
        }

        private static List<TypedItem> GenerateMessage<T>(IContext context, ModelActionOption modelAction)
        {
            List<TypedItem> items = new List<TypedItem>();
            items.Add(new TypedItem(XFConstants.Message.Verb, XFConstants.Message.VerbConstList[modelAction]) { Domain = XFConstants.Domain.Context });
            items.Add(new TypedItem(XFConstants.Context.Application, context.ApplicationContextKey) { Domain = XFConstants.Domain.Context });
            items.Add(new TypedItem(XFConstants.Context.USERIDENTITY, context.UserIdentity) { Domain = XFConstants.Domain.Context });
            items.Add(new TypedItem(XFConstants.Context.UICULTURE, context.UICulture) { Domain = XFConstants.Domain.Context });
            items.Add(new TypedItem(XFConstants.Context.ZONE, context.Zone) { Domain = XFConstants.Domain.Context });
            if (context.Claims != null && context.Claims.Count() > 0)
            {
                int i = 1;
                foreach (var item in context.Claims)
                {
                    items.Add(new TypedItem(String.Format("{0}.{1}", XFConstants.Context.Claim, i++), item)
                    {
                        Domain = XFConstants.Domain.Claims
                    });
                }
            }
            items.Add(new TypedItem(XFConstants.Context.INSTANCEIDENTIFIER, Environment.MachineName) { Domain = XFConstants.Domain.Context });
            items.Add(new TypedItem(XFConstants.Context.RequestBegin, DateTime.Now) { Domain = XFConstants.Domain.Metrics });
            items.Add(new TypedItem(XFConstants.Context.Model, GetModelType<T>().FullName) { Domain = XFConstants.Domain.Context });
            return items;
        }


    }
}
