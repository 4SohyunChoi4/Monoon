#include "pch-c.h"
#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include "codegen/il2cpp-codegen-metadata.h"





// 0x00000001 System.Threading.Tasks.Task`1<TResult> System.Threading.Tasks.TaskExtensions::Unwrap(System.Threading.Tasks.Task`1<System.Threading.Tasks.Task`1<TResult>>)
// 0x00000002 System.Exception System.Linq.Error::ArgumentNull(System.String)
extern void Error_ArgumentNull_m0EDA0D46D72CA692518E3E2EB75B48044D8FD41E (void);
// 0x00000003 System.Exception System.Linq.Error::MoreThanOneMatch()
extern void Error_MoreThanOneMatch_m4C4756AF34A76EF12F3B2B6D8C78DE547F0FBCF8 (void);
// 0x00000004 System.Exception System.Linq.Error::NoElements()
extern void Error_NoElements_mB89E91246572F009281D79730950808F17C3F353 (void);
// 0x00000005 System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable::Where(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>)
// 0x00000006 System.Func`2<TSource,System.Boolean> System.Linq.Enumerable::CombinePredicates(System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,System.Boolean>)
// 0x00000007 System.Linq.IOrderedEnumerable`1<TSource> System.Linq.Enumerable::OrderBy(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,TKey>)
// 0x00000008 System.Linq.IOrderedEnumerable`1<TSource> System.Linq.Enumerable::ThenBy(System.Linq.IOrderedEnumerable`1<TSource>,System.Func`2<TSource,TKey>)
// 0x00000009 TSource[] System.Linq.Enumerable::ToArray(System.Collections.Generic.IEnumerable`1<TSource>)
// 0x0000000A System.Collections.Generic.List`1<TSource> System.Linq.Enumerable::ToList(System.Collections.Generic.IEnumerable`1<TSource>)
// 0x0000000B TSource System.Linq.Enumerable::First(System.Collections.Generic.IEnumerable`1<TSource>)
// 0x0000000C TSource System.Linq.Enumerable::Last(System.Collections.Generic.IEnumerable`1<TSource>)
// 0x0000000D TSource System.Linq.Enumerable::SingleOrDefault(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>)
// 0x0000000E System.Boolean System.Linq.Enumerable::Any(System.Collections.Generic.IEnumerable`1<TSource>)
// 0x0000000F System.Boolean System.Linq.Enumerable::Any(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>)
// 0x00000010 System.Int32 System.Linq.Enumerable::Count(System.Collections.Generic.IEnumerable`1<TSource>)
// 0x00000011 System.Boolean System.Linq.Enumerable::Contains(System.Collections.Generic.IEnumerable`1<TSource>,TSource)
// 0x00000012 System.Boolean System.Linq.Enumerable::Contains(System.Collections.Generic.IEnumerable`1<TSource>,TSource,System.Collections.Generic.IEqualityComparer`1<TSource>)
// 0x00000013 System.Void System.Linq.Enumerable/Iterator`1::.ctor()
// 0x00000014 TSource System.Linq.Enumerable/Iterator`1::get_Current()
// 0x00000015 System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/Iterator`1::Clone()
// 0x00000016 System.Void System.Linq.Enumerable/Iterator`1::Dispose()
// 0x00000017 System.Collections.Generic.IEnumerator`1<TSource> System.Linq.Enumerable/Iterator`1::GetEnumerator()
// 0x00000018 System.Boolean System.Linq.Enumerable/Iterator`1::MoveNext()
// 0x00000019 System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/Iterator`1::Where(System.Func`2<TSource,System.Boolean>)
// 0x0000001A System.Object System.Linq.Enumerable/Iterator`1::System.Collections.IEnumerator.get_Current()
// 0x0000001B System.Collections.IEnumerator System.Linq.Enumerable/Iterator`1::System.Collections.IEnumerable.GetEnumerator()
// 0x0000001C System.Void System.Linq.Enumerable/Iterator`1::System.Collections.IEnumerator.Reset()
// 0x0000001D System.Void System.Linq.Enumerable/WhereEnumerableIterator`1::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>)
// 0x0000001E System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1::Clone()
// 0x0000001F System.Void System.Linq.Enumerable/WhereEnumerableIterator`1::Dispose()
// 0x00000020 System.Boolean System.Linq.Enumerable/WhereEnumerableIterator`1::MoveNext()
// 0x00000021 System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1::Where(System.Func`2<TSource,System.Boolean>)
// 0x00000022 System.Void System.Linq.Enumerable/WhereArrayIterator`1::.ctor(TSource[],System.Func`2<TSource,System.Boolean>)
// 0x00000023 System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereArrayIterator`1::Clone()
// 0x00000024 System.Boolean System.Linq.Enumerable/WhereArrayIterator`1::MoveNext()
// 0x00000025 System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereArrayIterator`1::Where(System.Func`2<TSource,System.Boolean>)
// 0x00000026 System.Void System.Linq.Enumerable/WhereListIterator`1::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>)
// 0x00000027 System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereListIterator`1::Clone()
// 0x00000028 System.Boolean System.Linq.Enumerable/WhereListIterator`1::MoveNext()
// 0x00000029 System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereListIterator`1::Where(System.Func`2<TSource,System.Boolean>)
// 0x0000002A System.Void System.Linq.Enumerable/<>c__DisplayClass6_0`1::.ctor()
// 0x0000002B System.Boolean System.Linq.Enumerable/<>c__DisplayClass6_0`1::<CombinePredicates>b__0(TSource)
// 0x0000002C System.Linq.IOrderedEnumerable`1<TElement> System.Linq.IOrderedEnumerable`1::CreateOrderedEnumerable(System.Func`2<TElement,TKey>,System.Collections.Generic.IComparer`1<TKey>,System.Boolean)
// 0x0000002D System.Collections.Generic.IEnumerator`1<TElement> System.Linq.OrderedEnumerable`1::GetEnumerator()
// 0x0000002E System.Linq.EnumerableSorter`1<TElement> System.Linq.OrderedEnumerable`1::GetEnumerableSorter(System.Linq.EnumerableSorter`1<TElement>)
// 0x0000002F System.Collections.IEnumerator System.Linq.OrderedEnumerable`1::System.Collections.IEnumerable.GetEnumerator()
// 0x00000030 System.Linq.IOrderedEnumerable`1<TElement> System.Linq.OrderedEnumerable`1::System.Linq.IOrderedEnumerable<TElement>.CreateOrderedEnumerable(System.Func`2<TElement,TKey>,System.Collections.Generic.IComparer`1<TKey>,System.Boolean)
// 0x00000031 System.Void System.Linq.OrderedEnumerable`1::.ctor()
// 0x00000032 System.Void System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::.ctor(System.Int32)
// 0x00000033 System.Void System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::System.IDisposable.Dispose()
// 0x00000034 System.Boolean System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::MoveNext()
// 0x00000035 TElement System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::System.Collections.Generic.IEnumerator<TElement>.get_Current()
// 0x00000036 System.Void System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::System.Collections.IEnumerator.Reset()
// 0x00000037 System.Object System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::System.Collections.IEnumerator.get_Current()
// 0x00000038 System.Void System.Linq.OrderedEnumerable`2::.ctor(System.Collections.Generic.IEnumerable`1<TElement>,System.Func`2<TElement,TKey>,System.Collections.Generic.IComparer`1<TKey>,System.Boolean)
// 0x00000039 System.Linq.EnumerableSorter`1<TElement> System.Linq.OrderedEnumerable`2::GetEnumerableSorter(System.Linq.EnumerableSorter`1<TElement>)
// 0x0000003A System.Void System.Linq.EnumerableSorter`1::ComputeKeys(TElement[],System.Int32)
// 0x0000003B System.Int32 System.Linq.EnumerableSorter`1::CompareKeys(System.Int32,System.Int32)
// 0x0000003C System.Int32[] System.Linq.EnumerableSorter`1::Sort(TElement[],System.Int32)
// 0x0000003D System.Void System.Linq.EnumerableSorter`1::QuickSort(System.Int32[],System.Int32,System.Int32)
// 0x0000003E System.Void System.Linq.EnumerableSorter`1::.ctor()
// 0x0000003F System.Void System.Linq.EnumerableSorter`2::.ctor(System.Func`2<TElement,TKey>,System.Collections.Generic.IComparer`1<TKey>,System.Boolean,System.Linq.EnumerableSorter`1<TElement>)
// 0x00000040 System.Void System.Linq.EnumerableSorter`2::ComputeKeys(TElement[],System.Int32)
// 0x00000041 System.Int32 System.Linq.EnumerableSorter`2::CompareKeys(System.Int32,System.Int32)
// 0x00000042 System.Void System.Linq.Buffer`1::.ctor(System.Collections.Generic.IEnumerable`1<TElement>)
// 0x00000043 TElement[] System.Linq.Buffer`1::ToArray()
// 0x00000044 System.Void System.Collections.Generic.HashSet`1::.ctor()
// 0x00000045 System.Void System.Collections.Generic.HashSet`1::.ctor(System.Collections.Generic.IEqualityComparer`1<T>)
// 0x00000046 System.Void System.Collections.Generic.HashSet`1::.ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)
// 0x00000047 System.Void System.Collections.Generic.HashSet`1::System.Collections.Generic.ICollection<T>.Add(T)
// 0x00000048 System.Void System.Collections.Generic.HashSet`1::Clear()
// 0x00000049 System.Boolean System.Collections.Generic.HashSet`1::Contains(T)
// 0x0000004A System.Void System.Collections.Generic.HashSet`1::CopyTo(T[],System.Int32)
// 0x0000004B System.Boolean System.Collections.Generic.HashSet`1::Remove(T)
// 0x0000004C System.Int32 System.Collections.Generic.HashSet`1::get_Count()
// 0x0000004D System.Boolean System.Collections.Generic.HashSet`1::System.Collections.Generic.ICollection<T>.get_IsReadOnly()
// 0x0000004E System.Collections.Generic.HashSet`1/Enumerator<T> System.Collections.Generic.HashSet`1::GetEnumerator()
// 0x0000004F System.Collections.Generic.IEnumerator`1<T> System.Collections.Generic.HashSet`1::System.Collections.Generic.IEnumerable<T>.GetEnumerator()
// 0x00000050 System.Collections.IEnumerator System.Collections.Generic.HashSet`1::System.Collections.IEnumerable.GetEnumerator()
// 0x00000051 System.Void System.Collections.Generic.HashSet`1::GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)
// 0x00000052 System.Void System.Collections.Generic.HashSet`1::OnDeserialization(System.Object)
// 0x00000053 System.Boolean System.Collections.Generic.HashSet`1::Add(T)
// 0x00000054 System.Void System.Collections.Generic.HashSet`1::CopyTo(T[])
// 0x00000055 System.Void System.Collections.Generic.HashSet`1::CopyTo(T[],System.Int32,System.Int32)
// 0x00000056 System.Void System.Collections.Generic.HashSet`1::Initialize(System.Int32)
// 0x00000057 System.Void System.Collections.Generic.HashSet`1::IncreaseCapacity()
// 0x00000058 System.Void System.Collections.Generic.HashSet`1::SetCapacity(System.Int32)
// 0x00000059 System.Boolean System.Collections.Generic.HashSet`1::AddIfNotPresent(T)
// 0x0000005A System.Int32 System.Collections.Generic.HashSet`1::InternalGetHashCode(T)
// 0x0000005B System.Void System.Collections.Generic.HashSet`1/Enumerator::.ctor(System.Collections.Generic.HashSet`1<T>)
// 0x0000005C System.Void System.Collections.Generic.HashSet`1/Enumerator::Dispose()
// 0x0000005D System.Boolean System.Collections.Generic.HashSet`1/Enumerator::MoveNext()
// 0x0000005E T System.Collections.Generic.HashSet`1/Enumerator::get_Current()
// 0x0000005F System.Object System.Collections.Generic.HashSet`1/Enumerator::System.Collections.IEnumerator.get_Current()
// 0x00000060 System.Void System.Collections.Generic.HashSet`1/Enumerator::System.Collections.IEnumerator.Reset()
// 0x00000061 System.Void System.Collections.Generic.ICollectionDebugView`1::.ctor(System.Collections.Generic.ICollection`1<T>)
// 0x00000062 T[] System.Collections.Generic.ICollectionDebugView`1::get_Items()
static Il2CppMethodPointer s_methodPointers[98] = 
{
	NULL,
	Error_ArgumentNull_m0EDA0D46D72CA692518E3E2EB75B48044D8FD41E,
	Error_MoreThanOneMatch_m4C4756AF34A76EF12F3B2B6D8C78DE547F0FBCF8,
	Error_NoElements_mB89E91246572F009281D79730950808F17C3F353,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
};
static const int32_t s_InvokerIndices[98] = 
{
	-1,
	2269,
	2353,
	2353,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
};
static const Il2CppTokenRangePair s_rgctxIndices[30] = 
{
	{ 0x02000005, { 48, 4 } },
	{ 0x02000006, { 52, 9 } },
	{ 0x02000007, { 61, 7 } },
	{ 0x02000008, { 68, 10 } },
	{ 0x02000009, { 78, 1 } },
	{ 0x0200000B, { 79, 3 } },
	{ 0x0200000C, { 84, 5 } },
	{ 0x0200000D, { 89, 7 } },
	{ 0x0200000E, { 96, 3 } },
	{ 0x0200000F, { 99, 7 } },
	{ 0x02000010, { 106, 4 } },
	{ 0x02000011, { 110, 21 } },
	{ 0x02000013, { 131, 2 } },
	{ 0x02000014, { 133, 2 } },
	{ 0x06000001, { 0, 1 } },
	{ 0x06000005, { 1, 10 } },
	{ 0x06000006, { 11, 5 } },
	{ 0x06000007, { 16, 2 } },
	{ 0x06000008, { 18, 1 } },
	{ 0x06000009, { 19, 3 } },
	{ 0x0600000A, { 22, 2 } },
	{ 0x0600000B, { 24, 4 } },
	{ 0x0600000C, { 28, 4 } },
	{ 0x0600000D, { 32, 3 } },
	{ 0x0600000E, { 35, 1 } },
	{ 0x0600000F, { 36, 3 } },
	{ 0x06000010, { 39, 2 } },
	{ 0x06000011, { 41, 2 } },
	{ 0x06000012, { 43, 5 } },
	{ 0x06000030, { 82, 2 } },
};
static const Il2CppRGCTXDefinition s_rgctxValues[135] = 
{
	{ (Il2CppRGCTXDataType)3, 9050 },
	{ (Il2CppRGCTXDataType)2, 1262 },
	{ (Il2CppRGCTXDataType)3, 4211 },
	{ (Il2CppRGCTXDataType)2, 2105 },
	{ (Il2CppRGCTXDataType)2, 1809 },
	{ (Il2CppRGCTXDataType)3, 7273 },
	{ (Il2CppRGCTXDataType)2, 1339 },
	{ (Il2CppRGCTXDataType)2, 1813 },
	{ (Il2CppRGCTXDataType)3, 7286 },
	{ (Il2CppRGCTXDataType)2, 1811 },
	{ (Il2CppRGCTXDataType)3, 7279 },
	{ (Il2CppRGCTXDataType)2, 450 },
	{ (Il2CppRGCTXDataType)3, 30 },
	{ (Il2CppRGCTXDataType)3, 31 },
	{ (Il2CppRGCTXDataType)2, 844 },
	{ (Il2CppRGCTXDataType)3, 3104 },
	{ (Il2CppRGCTXDataType)2, 1576 },
	{ (Il2CppRGCTXDataType)3, 6141 },
	{ (Il2CppRGCTXDataType)3, 3436 },
	{ (Il2CppRGCTXDataType)2, 518 },
	{ (Il2CppRGCTXDataType)3, 458 },
	{ (Il2CppRGCTXDataType)3, 459 },
	{ (Il2CppRGCTXDataType)2, 1340 },
	{ (Il2CppRGCTXDataType)3, 4506 },
	{ (Il2CppRGCTXDataType)2, 1205 },
	{ (Il2CppRGCTXDataType)2, 928 },
	{ (Il2CppRGCTXDataType)2, 1024 },
	{ (Il2CppRGCTXDataType)2, 1075 },
	{ (Il2CppRGCTXDataType)2, 1206 },
	{ (Il2CppRGCTXDataType)2, 929 },
	{ (Il2CppRGCTXDataType)2, 1025 },
	{ (Il2CppRGCTXDataType)2, 1076 },
	{ (Il2CppRGCTXDataType)2, 1026 },
	{ (Il2CppRGCTXDataType)2, 1077 },
	{ (Il2CppRGCTXDataType)3, 3105 },
	{ (Il2CppRGCTXDataType)2, 1017 },
	{ (Il2CppRGCTXDataType)2, 1018 },
	{ (Il2CppRGCTXDataType)2, 1073 },
	{ (Il2CppRGCTXDataType)3, 3103 },
	{ (Il2CppRGCTXDataType)2, 927 },
	{ (Il2CppRGCTXDataType)2, 1023 },
	{ (Il2CppRGCTXDataType)2, 926 },
	{ (Il2CppRGCTXDataType)3, 8712 },
	{ (Il2CppRGCTXDataType)3, 2742 },
	{ (Il2CppRGCTXDataType)2, 740 },
	{ (Il2CppRGCTXDataType)2, 1020 },
	{ (Il2CppRGCTXDataType)2, 1074 },
	{ (Il2CppRGCTXDataType)2, 1122 },
	{ (Il2CppRGCTXDataType)3, 4212 },
	{ (Il2CppRGCTXDataType)3, 4214 },
	{ (Il2CppRGCTXDataType)2, 325 },
	{ (Il2CppRGCTXDataType)3, 4213 },
	{ (Il2CppRGCTXDataType)3, 4222 },
	{ (Il2CppRGCTXDataType)2, 1265 },
	{ (Il2CppRGCTXDataType)2, 1812 },
	{ (Il2CppRGCTXDataType)3, 7280 },
	{ (Il2CppRGCTXDataType)3, 4223 },
	{ (Il2CppRGCTXDataType)2, 1050 },
	{ (Il2CppRGCTXDataType)2, 1095 },
	{ (Il2CppRGCTXDataType)3, 3110 },
	{ (Il2CppRGCTXDataType)3, 8707 },
	{ (Il2CppRGCTXDataType)3, 4215 },
	{ (Il2CppRGCTXDataType)2, 1264 },
	{ (Il2CppRGCTXDataType)2, 1810 },
	{ (Il2CppRGCTXDataType)3, 7274 },
	{ (Il2CppRGCTXDataType)3, 3109 },
	{ (Il2CppRGCTXDataType)3, 4216 },
	{ (Il2CppRGCTXDataType)3, 8706 },
	{ (Il2CppRGCTXDataType)3, 4229 },
	{ (Il2CppRGCTXDataType)2, 1266 },
	{ (Il2CppRGCTXDataType)2, 1814 },
	{ (Il2CppRGCTXDataType)3, 7287 },
	{ (Il2CppRGCTXDataType)3, 4547 },
	{ (Il2CppRGCTXDataType)3, 2302 },
	{ (Il2CppRGCTXDataType)3, 3111 },
	{ (Il2CppRGCTXDataType)3, 2301 },
	{ (Il2CppRGCTXDataType)3, 4230 },
	{ (Il2CppRGCTXDataType)3, 8708 },
	{ (Il2CppRGCTXDataType)3, 3108 },
	{ (Il2CppRGCTXDataType)2, 451 },
	{ (Il2CppRGCTXDataType)3, 34 },
	{ (Il2CppRGCTXDataType)3, 6128 },
	{ (Il2CppRGCTXDataType)2, 1577 },
	{ (Il2CppRGCTXDataType)3, 6142 },
	{ (Il2CppRGCTXDataType)2, 519 },
	{ (Il2CppRGCTXDataType)3, 460 },
	{ (Il2CppRGCTXDataType)3, 6134 },
	{ (Il2CppRGCTXDataType)3, 2282 },
	{ (Il2CppRGCTXDataType)2, 341 },
	{ (Il2CppRGCTXDataType)3, 6129 },
	{ (Il2CppRGCTXDataType)2, 1573 },
	{ (Il2CppRGCTXDataType)3, 501 },
	{ (Il2CppRGCTXDataType)2, 533 },
	{ (Il2CppRGCTXDataType)2, 727 },
	{ (Il2CppRGCTXDataType)3, 2288 },
	{ (Il2CppRGCTXDataType)3, 6130 },
	{ (Il2CppRGCTXDataType)3, 2277 },
	{ (Il2CppRGCTXDataType)3, 2278 },
	{ (Il2CppRGCTXDataType)3, 2276 },
	{ (Il2CppRGCTXDataType)3, 2279 },
	{ (Il2CppRGCTXDataType)2, 723 },
	{ (Il2CppRGCTXDataType)2, 2174 },
	{ (Il2CppRGCTXDataType)3, 3107 },
	{ (Il2CppRGCTXDataType)3, 2281 },
	{ (Il2CppRGCTXDataType)2, 1001 },
	{ (Il2CppRGCTXDataType)3, 2280 },
	{ (Il2CppRGCTXDataType)2, 930 },
	{ (Il2CppRGCTXDataType)2, 2128 },
	{ (Il2CppRGCTXDataType)2, 1029 },
	{ (Il2CppRGCTXDataType)2, 1078 },
	{ (Il2CppRGCTXDataType)3, 2758 },
	{ (Il2CppRGCTXDataType)2, 748 },
	{ (Il2CppRGCTXDataType)3, 3310 },
	{ (Il2CppRGCTXDataType)3, 3311 },
	{ (Il2CppRGCTXDataType)3, 3316 },
	{ (Il2CppRGCTXDataType)2, 1130 },
	{ (Il2CppRGCTXDataType)3, 3313 },
	{ (Il2CppRGCTXDataType)3, 8964 },
	{ (Il2CppRGCTXDataType)2, 728 },
	{ (Il2CppRGCTXDataType)3, 2295 },
	{ (Il2CppRGCTXDataType)1, 998 },
	{ (Il2CppRGCTXDataType)2, 2137 },
	{ (Il2CppRGCTXDataType)3, 3312 },
	{ (Il2CppRGCTXDataType)1, 2137 },
	{ (Il2CppRGCTXDataType)1, 1130 },
	{ (Il2CppRGCTXDataType)2, 2190 },
	{ (Il2CppRGCTXDataType)2, 2137 },
	{ (Il2CppRGCTXDataType)3, 3317 },
	{ (Il2CppRGCTXDataType)3, 3315 },
	{ (Il2CppRGCTXDataType)3, 3314 },
	{ (Il2CppRGCTXDataType)2, 218 },
	{ (Il2CppRGCTXDataType)3, 2303 },
	{ (Il2CppRGCTXDataType)2, 331 },
	{ (Il2CppRGCTXDataType)2, 936 },
	{ (Il2CppRGCTXDataType)2, 2139 },
};
extern const CustomAttributesCacheGenerator g_System_Core_AttributeGenerators[];
IL2CPP_EXTERN_C const Il2CppCodeGenModule g_System_Core_CodeGenModule;
const Il2CppCodeGenModule g_System_Core_CodeGenModule = 
{
	"System.Core.dll",
	98,
	s_methodPointers,
	0,
	NULL,
	s_InvokerIndices,
	0,
	NULL,
	30,
	s_rgctxIndices,
	135,
	s_rgctxValues,
	NULL,
	g_System_Core_AttributeGenerators,
	NULL, // module initializer,
	NULL,
	NULL,
	NULL,
};
