; ModuleID = 'obj/Release/android/marshal_methods.armeabi-v7a.ll'
source_filename = "obj/Release/android/marshal_methods.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android"


%struct.MonoImage = type opaque

%struct.MonoClass = type opaque

%struct.MarshalMethodsManagedClass = type {
	i32,; uint32_t token
	%struct.MonoClass*; MonoClass* klass
}

%struct.MarshalMethodName = type {
	i64,; uint64_t id
	i8*; char* name
}

%class._JNIEnv = type opaque

%class._jobject = type {
	i8; uint8_t b
}

%class._jclass = type {
	i8; uint8_t b
}

%class._jstring = type {
	i8; uint8_t b
}

%class._jthrowable = type {
	i8; uint8_t b
}

%class._jarray = type {
	i8; uint8_t b
}

%class._jobjectArray = type {
	i8; uint8_t b
}

%class._jbooleanArray = type {
	i8; uint8_t b
}

%class._jbyteArray = type {
	i8; uint8_t b
}

%class._jcharArray = type {
	i8; uint8_t b
}

%class._jshortArray = type {
	i8; uint8_t b
}

%class._jintArray = type {
	i8; uint8_t b
}

%class._jlongArray = type {
	i8; uint8_t b
}

%class._jfloatArray = type {
	i8; uint8_t b
}

%class._jdoubleArray = type {
	i8; uint8_t b
}

; assembly_image_cache
@assembly_image_cache = local_unnamed_addr global [0 x %struct.MonoImage*] zeroinitializer, align 4
; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = local_unnamed_addr constant [130 x i32] [
	i32 34715100, ; 0: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 48
	i32 57263871, ; 1: Xamarin.Forms.Core.dll => 0x369c6ff => 39
	i32 134690465, ; 2: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 56
	i32 182336117, ; 3: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 36
	i32 212497893, ; 4: Xamarin.Forms.Maps.Android => 0xcaa75e5 => 42
	i32 318968648, ; 5: Xamarin.AndroidX.Activity.dll => 0x13031348 => 19
	i32 319314094, ; 6: Xamarin.Forms.Maps => 0x130858ae => 43
	i32 321597661, ; 7: System.Numerics => 0x132b30dd => 16
	i32 342366114, ; 8: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 30
	i32 347068432, ; 9: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 10
	i32 442521989, ; 10: Xamarin.Essentials => 0x1a605985 => 38
	i32 450948140, ; 11: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 28
	i32 465846621, ; 12: mscorlib => 0x1bc4415d => 6
	i32 469710990, ; 13: System.dll => 0x1bff388e => 14
	i32 526420162, ; 14: System.Transactions.dll => 0x1f6088c2 => 62
	i32 527452488, ; 15: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 56
	i32 627609679, ; 16: Xamarin.AndroidX.CustomView => 0x2568904f => 26
	i32 691348768, ; 17: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 58
	i32 700284507, ; 18: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 53
	i32 720511267, ; 19: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 57
	i32 748832960, ; 20: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 8
	i32 809851609, ; 21: System.Drawing.Common.dll => 0x30455ad9 => 63
	i32 928116545, ; 22: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 48
	i32 956575887, ; 23: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 57
	i32 967690846, ; 24: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 30
	i32 974778368, ; 25: FormsViewGroup.dll => 0x3a19f000 => 2
	i32 1012816738, ; 26: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 35
	i32 1035644815, ; 27: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 21
	i32 1042160112, ; 28: Xamarin.Forms.Platform.dll => 0x3e1e19f0 => 45
	i32 1052210849, ; 29: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 32
	i32 1084122840, ; 30: Xamarin.Kotlin.StdLib => 0x409e66d8 => 55
	i32 1098259244, ; 31: System => 0x41761b2c => 14
	i32 1275534314, ; 32: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 58
	i32 1292207520, ; 33: SQLitePCLRaw.core.dll => 0x4d0585a0 => 9
	i32 1293217323, ; 34: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 27
	i32 1354490624, ; 35: Xamarin.Forms.GoogleMaps.dll => 0x50bbe300 => 41
	i32 1365406463, ; 36: System.ServiceModel.Internals.dll => 0x516272ff => 60
	i32 1371845985, ; 37: Xamarin.Forms.GoogleMaps.Android => 0x51c4b561 => 40
	i32 1376866003, ; 38: Xamarin.AndroidX.SavedState => 0x52114ed3 => 35
	i32 1406073936, ; 39: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 23
	i32 1411638395, ; 40: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 17
	i32 1460219004, ; 41: Xamarin.Forms.Xaml => 0x57092c7c => 46
	i32 1469204771, ; 42: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 20
	i32 1530663695, ; 43: Xamarin.Forms.Maps.dll => 0x5b3c130f => 43
	i32 1592978981, ; 44: System.Runtime.Serialization.dll => 0x5ef2ee25 => 1
	i32 1622152042, ; 45: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 33
	i32 1636350590, ; 46: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 25
	i32 1639515021, ; 47: System.Net.Http.dll => 0x61b9038d => 15
	i32 1658251792, ; 48: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 47
	i32 1698840827, ; 49: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 54
	i32 1711441057, ; 50: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 10
	i32 1729485958, ; 51: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 22
	i32 1766324549, ; 52: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 36
	i32 1776026572, ; 53: System.Core.dll => 0x69dc03cc => 13
	i32 1788241197, ; 54: Xamarin.AndroidX.Fragment => 0x6a96652d => 28
	i32 1808609942, ; 55: Xamarin.AndroidX.Loader => 0x6bcd3296 => 33
	i32 1813058853, ; 56: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 55
	i32 1813201214, ; 57: Xamarin.Google.Android.Material => 0x6c13413e => 47
	i32 1867746548, ; 58: Xamarin.Essentials.dll => 0x6f538cf4 => 38
	i32 1878053835, ; 59: Xamarin.Forms.Xaml.dll => 0x6ff0d3cb => 46
	i32 1881862856, ; 60: Xamarin.Forms.Maps.Android.dll => 0x702af2c8 => 42
	i32 1908813208, ; 61: Xamarin.GooglePlayServices.Basement => 0x71c62d98 => 50
	i32 1983156543, ; 62: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 54
	i32 2011961780, ; 63: System.Buffers.dll => 0x77ec19b4 => 12
	i32 2019465201, ; 64: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 32
	i32 2055257422, ; 65: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 31
	i32 2097448633, ; 66: Xamarin.AndroidX.Legacy.Support.Core.UI => 0x7d0486b9 => 29
	i32 2103459038, ; 67: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 11
	i32 2126786730, ; 68: Xamarin.Forms.Platform.Android => 0x7ec430aa => 44
	i32 2129483829, ; 69: Xamarin.GooglePlayServices.Base.dll => 0x7eed5835 => 49
	i32 2201107256, ; 70: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 59
	i32 2201231467, ; 71: System.Net.Http => 0x8334206b => 15
	i32 2238958092, ; 72: GeoTimeTrack => 0x8573ca0c => 3
	i32 2279755925, ; 73: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 34
	i32 2445024337, ; 74: Xamarin.Forms.GoogleMaps.Android.dll => 0x91bc1c51 => 40
	i32 2465273461, ; 75: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 8
	i32 2475788418, ; 76: Java.Interop.dll => 0x93918882 => 4
	i32 2605712449, ; 77: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 59
	i32 2620871830, ; 78: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 25
	i32 2732626843, ; 79: Xamarin.AndroidX.Activity => 0xa2e0939b => 19
	i32 2737747696, ; 80: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 20
	i32 2766581644, ; 81: Xamarin.Forms.Core => 0xa4e6af8c => 39
	i32 2770495804, ; 82: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 53
	i32 2778768386, ; 83: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 37
	i32 2810250172, ; 84: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 23
	i32 2819470561, ; 85: System.Xml.dll => 0xa80db4e1 => 18
	i32 2847418871, ; 86: Xamarin.GooglePlayServices.Base => 0xa9b829f7 => 49
	i32 2853208004, ; 87: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 37
	i32 2905242038, ; 88: mscorlib.dll => 0xad2a79b6 => 6
	i32 2955285727, ; 89: GeoTimeTrack.Android => 0xb02614df => 0
	i32 2978675010, ; 90: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 27
	i32 3017076677, ; 91: Xamarin.GooglePlayServices.Maps => 0xb3d4efc5 => 51
	i32 3044182254, ; 92: FormsViewGroup => 0xb57288ee => 2
	i32 3058099980, ; 93: Xamarin.GooglePlayServices.Tasks => 0xb646e70c => 52
	i32 3111772706, ; 94: System.Runtime.Serialization => 0xb979e222 => 1
	i32 3204380047, ; 95: System.Data.dll => 0xbefef58f => 61
	i32 3230466174, ; 96: Xamarin.GooglePlayServices.Basement.dll => 0xc08d007e => 50
	i32 3247949154, ; 97: Mono.Security => 0xc197c562 => 64
	i32 3258312781, ; 98: Xamarin.AndroidX.CardView => 0xc235e84d => 22
	i32 3286872994, ; 99: SQLite-net.dll => 0xc3e9b3a2 => 7
	i32 3317135071, ; 100: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 26
	i32 3317144872, ; 101: System.Data => 0xc5b79d28 => 61
	i32 3353484488, ; 102: Xamarin.AndroidX.Legacy.Support.Core.UI.dll => 0xc7e21cc8 => 29
	i32 3360279109, ; 103: SQLitePCLRaw.core => 0xc849ca45 => 9
	i32 3362522851, ; 104: Xamarin.AndroidX.Core => 0xc86c06e3 => 24
	i32 3366347497, ; 105: Java.Interop => 0xc8a662e9 => 4
	i32 3374999561, ; 106: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 34
	i32 3395150330, ; 107: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 17
	i32 3404865022, ; 108: System.ServiceModel.Internals => 0xcaf21dfe => 60
	i32 3429136800, ; 109: System.Xml => 0xcc6479a0 => 18
	i32 3459516321, ; 110: Xamarin.Forms.GoogleMaps => 0xce3407a1 => 41
	i32 3476120550, ; 111: Mono.Android => 0xcf3163e6 => 5
	i32 3486566296, ; 112: System.Transactions => 0xcfd0c798 => 62
	i32 3502435540, ; 113: GeoTimeTrack.Android.dll => 0xd0c2ecd4 => 0
	i32 3536029504, ; 114: Xamarin.Forms.Platform.Android.dll => 0xd2c38740 => 44
	i32 3632359727, ; 115: Xamarin.Forms.Platform => 0xd881692f => 45
	i32 3641597786, ; 116: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 31
	i32 3672681054, ; 117: Mono.Android.dll => 0xdae8aa5e => 5
	i32 3689375977, ; 118: System.Drawing.Common => 0xdbe768e9 => 63
	i32 3754567612, ; 119: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 11
	i32 3829621856, ; 120: System.Numerics.dll => 0xe4436460 => 16
	i32 3876362041, ; 121: SQLite-net => 0xe70c9739 => 7
	i32 3896760992, ; 122: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 24
	i32 3955647286, ; 123: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 21
	i32 3957819070, ; 124: GeoTimeTrack.dll => 0xebe786be => 3
	i32 3970018735, ; 125: Xamarin.GooglePlayServices.Tasks.dll => 0xeca1adaf => 52
	i32 4105002889, ; 126: Mono.Security.dll => 0xf4ad5f89 => 64
	i32 4151237749, ; 127: System.Core => 0xf76edc75 => 13
	i32 4260525087, ; 128: System.Buffers => 0xfdf2741f => 12
	i32 4278134329 ; 129: Xamarin.GooglePlayServices.Maps.dll => 0xfeff2639 => 51
], align 4
@assembly_image_cache_indices = local_unnamed_addr constant [130 x i32] [
	i32 48, i32 39, i32 56, i32 36, i32 42, i32 19, i32 43, i32 16, ; 0..7
	i32 30, i32 10, i32 38, i32 28, i32 6, i32 14, i32 62, i32 56, ; 8..15
	i32 26, i32 58, i32 53, i32 57, i32 8, i32 63, i32 48, i32 57, ; 16..23
	i32 30, i32 2, i32 35, i32 21, i32 45, i32 32, i32 55, i32 14, ; 24..31
	i32 58, i32 9, i32 27, i32 41, i32 60, i32 40, i32 35, i32 23, ; 32..39
	i32 17, i32 46, i32 20, i32 43, i32 1, i32 33, i32 25, i32 15, ; 40..47
	i32 47, i32 54, i32 10, i32 22, i32 36, i32 13, i32 28, i32 33, ; 48..55
	i32 55, i32 47, i32 38, i32 46, i32 42, i32 50, i32 54, i32 12, ; 56..63
	i32 32, i32 31, i32 29, i32 11, i32 44, i32 49, i32 59, i32 15, ; 64..71
	i32 3, i32 34, i32 40, i32 8, i32 4, i32 59, i32 25, i32 19, ; 72..79
	i32 20, i32 39, i32 53, i32 37, i32 23, i32 18, i32 49, i32 37, ; 80..87
	i32 6, i32 0, i32 27, i32 51, i32 2, i32 52, i32 1, i32 61, ; 88..95
	i32 50, i32 64, i32 22, i32 7, i32 26, i32 61, i32 29, i32 9, ; 96..103
	i32 24, i32 4, i32 34, i32 17, i32 60, i32 18, i32 41, i32 5, ; 104..111
	i32 62, i32 0, i32 44, i32 45, i32 31, i32 5, i32 63, i32 11, ; 112..119
	i32 16, i32 7, i32 24, i32 21, i32 3, i32 52, i32 64, i32 13, ; 120..127
	i32 12, i32 51 ; 128..129
], align 4

@marshal_methods_number_of_classes = local_unnamed_addr constant i32 0, align 4

; marshal_methods_class_cache
@marshal_methods_class_cache = global [0 x %struct.MarshalMethodsManagedClass] [
], align 4; end of 'marshal_methods_class_cache' array


@get_function_pointer = internal unnamed_addr global void (i32, i32, i32, i8**)* null, align 4

; Function attributes: "frame-pointer"="all" "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" uwtable willreturn writeonly
define void @xamarin_app_init (void (i32, i32, i32, i8**)* %fn) local_unnamed_addr #0
{
	store void (i32, i32, i32, i8**)* %fn, void (i32, i32, i32, i8**)** @get_function_pointer, align 4
	ret void
}

; Names of classes in which marshal methods reside
@mm_class_names = local_unnamed_addr constant [0 x i8*] zeroinitializer, align 4
@__MarshalMethodName_name.0 = internal constant [1 x i8] c"\00", align 1

; mm_method_names
@mm_method_names = local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	; 0
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		i8* getelementptr inbounds ([1 x i8], [1 x i8]* @__MarshalMethodName_name.0, i32 0, i32 0); name
	}
], align 8; end of 'mm_method_names' array


attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable willreturn writeonly "frame-pointer"="all" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }
attributes #1 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable "frame-pointer"="all" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1, !2}
!llvm.ident = !{!3}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"min_enum_size", i32 4}
!3 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
!llvm.linker.options = !{}
