; ModuleID = 'obj\Debug\130\android\marshal_methods.armeabi-v7a.ll'
source_filename = "obj\Debug\130\android\marshal_methods.armeabi-v7a.ll"
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
@assembly_image_cache_hashes = local_unnamed_addr constant [230 x i32] [
	i32 32687329, ; 0: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 56
	i32 34715100, ; 1: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 92
	i32 57263871, ; 2: Xamarin.Forms.Core.dll => 0x369c6ff => 83
	i32 101534019, ; 3: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 72
	i32 120558881, ; 4: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 72
	i32 134690465, ; 5: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 100
	i32 165246403, ; 6: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 33
	i32 182336117, ; 7: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 74
	i32 209399409, ; 8: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 31
	i32 212497893, ; 9: Xamarin.Forms.Maps.Android => 0xcaa75e5 => 86
	i32 230216969, ; 10: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 50
	i32 232815796, ; 11: System.Web.Services => 0xde07cb4 => 113
	i32 261689757, ; 12: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 36
	i32 278686392, ; 13: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 54
	i32 280482487, ; 14: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 48
	i32 318968648, ; 15: Xamarin.AndroidX.Activity.dll => 0x13031348 => 23
	i32 319314094, ; 16: Xamarin.Forms.Maps => 0x130858ae => 87
	i32 321597661, ; 17: System.Numerics => 0x132b30dd => 17
	i32 342366114, ; 18: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 52
	i32 347068432, ; 19: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 10
	i32 385762202, ; 20: System.Memory.dll => 0x16fe439a => 15
	i32 441335492, ; 21: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 35
	i32 442521989, ; 22: Xamarin.Essentials => 0x1a605985 => 82
	i32 450948140, ; 23: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 47
	i32 465846621, ; 24: mscorlib => 0x1bc4415d => 6
	i32 469710990, ; 25: System.dll => 0x1bff388e => 14
	i32 476646585, ; 26: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 48
	i32 486930444, ; 27: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 60
	i32 526420162, ; 28: System.Transactions.dll => 0x1f6088c2 => 107
	i32 527452488, ; 29: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 100
	i32 605376203, ; 30: System.IO.Compression.FileSystem => 0x24154ecb => 111
	i32 627609679, ; 31: Xamarin.AndroidX.CustomView => 0x2568904f => 41
	i32 639843206, ; 32: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 46
	i32 663517072, ; 33: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 79
	i32 666292255, ; 34: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 28
	i32 690569205, ; 35: System.Xml.Linq.dll => 0x29293ff5 => 22
	i32 691348768, ; 36: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 102
	i32 700284507, ; 37: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 97
	i32 720511267, ; 38: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 101
	i32 748832960, ; 39: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 8
	i32 775507847, ; 40: System.IO.Compression => 0x2e394f87 => 110
	i32 809851609, ; 41: System.Drawing.Common.dll => 0x30455ad9 => 109
	i32 843511501, ; 42: Xamarin.AndroidX.Print => 0x3246f6cd => 67
	i32 928116545, ; 43: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 92
	i32 956575887, ; 44: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 101
	i32 967690846, ; 45: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 52
	i32 974778368, ; 46: FormsViewGroup.dll => 0x3a19f000 => 2
	i32 1012816738, ; 47: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 71
	i32 1035644815, ; 48: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 27
	i32 1042160112, ; 49: Xamarin.Forms.Platform.dll => 0x3e1e19f0 => 89
	i32 1052210849, ; 50: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 57
	i32 1084122840, ; 51: Xamarin.Kotlin.StdLib => 0x409e66d8 => 99
	i32 1098259244, ; 52: System => 0x41761b2c => 14
	i32 1175144683, ; 53: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 77
	i32 1178241025, ; 54: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 64
	i32 1204270330, ; 55: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 28
	i32 1264511973, ; 56: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 73
	i32 1267360935, ; 57: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 78
	i32 1275534314, ; 58: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 102
	i32 1292207520, ; 59: SQLitePCLRaw.core.dll => 0x4d0585a0 => 9
	i32 1293217323, ; 60: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 43
	i32 1354490624, ; 61: Xamarin.Forms.GoogleMaps.dll => 0x50bbe300 => 85
	i32 1365406463, ; 62: System.ServiceModel.Internals.dll => 0x516272ff => 104
	i32 1371845985, ; 63: Xamarin.Forms.GoogleMaps.Android => 0x51c4b561 => 84
	i32 1376866003, ; 64: Xamarin.AndroidX.SavedState => 0x52114ed3 => 71
	i32 1395857551, ; 65: Xamarin.AndroidX.Media.dll => 0x5333188f => 61
	i32 1406073936, ; 66: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 37
	i32 1411638395, ; 67: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 19
	i32 1460219004, ; 68: Xamarin.Forms.Xaml => 0x57092c7c => 90
	i32 1462112819, ; 69: System.IO.Compression.dll => 0x57261233 => 110
	i32 1469204771, ; 70: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 26
	i32 1530663695, ; 71: Xamarin.Forms.Maps.dll => 0x5b3c130f => 87
	i32 1582372066, ; 72: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 42
	i32 1592978981, ; 73: System.Runtime.Serialization.dll => 0x5ef2ee25 => 1
	i32 1622152042, ; 74: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 59
	i32 1624863272, ; 75: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 81
	i32 1635184631, ; 76: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 46
	i32 1636350590, ; 77: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 40
	i32 1639515021, ; 78: System.Net.Http.dll => 0x61b9038d => 16
	i32 1657153582, ; 79: System.Runtime => 0x62c6282e => 20
	i32 1658241508, ; 80: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 75
	i32 1658251792, ; 81: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 91
	i32 1670060433, ; 82: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 36
	i32 1698840827, ; 83: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 98
	i32 1711441057, ; 84: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 10
	i32 1729485958, ; 85: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 32
	i32 1766324549, ; 86: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 74
	i32 1776026572, ; 87: System.Core.dll => 0x69dc03cc => 13
	i32 1788241197, ; 88: Xamarin.AndroidX.Fragment => 0x6a96652d => 47
	i32 1808609942, ; 89: Xamarin.AndroidX.Loader => 0x6bcd3296 => 59
	i32 1813058853, ; 90: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 99
	i32 1813201214, ; 91: Xamarin.Google.Android.Material => 0x6c13413e => 91
	i32 1818569960, ; 92: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 65
	i32 1867746548, ; 93: Xamarin.Essentials.dll => 0x6f538cf4 => 82
	i32 1878053835, ; 94: Xamarin.Forms.Xaml.dll => 0x6ff0d3cb => 90
	i32 1881862856, ; 95: Xamarin.Forms.Maps.Android.dll => 0x702af2c8 => 86
	i32 1885316902, ; 96: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 29
	i32 1908813208, ; 97: Xamarin.GooglePlayServices.Basement => 0x71c62d98 => 94
	i32 1919157823, ; 98: Xamarin.AndroidX.MultiDex.dll => 0x7264063f => 62
	i32 1983156543, ; 99: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 98
	i32 2011961780, ; 100: System.Buffers.dll => 0x77ec19b4 => 12
	i32 2019465201, ; 101: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 57
	i32 2055257422, ; 102: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 53
	i32 2079903147, ; 103: System.Runtime.dll => 0x7bf8cdab => 20
	i32 2090596640, ; 104: System.Numerics.Vectors => 0x7c9bf920 => 18
	i32 2097448633, ; 105: Xamarin.AndroidX.Legacy.Support.Core.UI => 0x7d0486b9 => 49
	i32 2103459038, ; 106: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 11
	i32 2126786730, ; 107: Xamarin.Forms.Platform.Android => 0x7ec430aa => 88
	i32 2129483829, ; 108: Xamarin.GooglePlayServices.Base.dll => 0x7eed5835 => 93
	i32 2201107256, ; 109: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 103
	i32 2201231467, ; 110: System.Net.Http => 0x8334206b => 16
	i32 2217644978, ; 111: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 77
	i32 2238958092, ; 112: GeoTimeTrack => 0x8573ca0c => 3
	i32 2244775296, ; 113: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 60
	i32 2256548716, ; 114: Xamarin.AndroidX.MultiDex => 0x8680336c => 62
	i32 2261435625, ; 115: Xamarin.AndroidX.Legacy.Support.V4.dll => 0x86cac4e9 => 51
	i32 2279755925, ; 116: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 69
	i32 2315684594, ; 117: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 24
	i32 2403452196, ; 118: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 45
	i32 2409053734, ; 119: Xamarin.AndroidX.Preference.dll => 0x8f973e26 => 66
	i32 2445024337, ; 120: Xamarin.Forms.GoogleMaps.Android.dll => 0x91bc1c51 => 84
	i32 2465273461, ; 121: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 8
	i32 2465532216, ; 122: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 35
	i32 2471841756, ; 123: netstandard.dll => 0x93554fdc => 105
	i32 2475788418, ; 124: Java.Interop.dll => 0x93918882 => 4
	i32 2501346920, ; 125: System.Data.DataSetExtensions => 0x95178668 => 108
	i32 2505896520, ; 126: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 56
	i32 2581819634, ; 127: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 78
	i32 2605712449, ; 128: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 103
	i32 2620871830, ; 129: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 40
	i32 2624644809, ; 130: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 44
	i32 2633051222, ; 131: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 54
	i32 2701096212, ; 132: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 75
	i32 2732626843, ; 133: Xamarin.AndroidX.Activity => 0xa2e0939b => 23
	i32 2737747696, ; 134: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 26
	i32 2766581644, ; 135: Xamarin.Forms.Core => 0xa4e6af8c => 83
	i32 2770495804, ; 136: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 97
	i32 2778768386, ; 137: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 80
	i32 2779977773, ; 138: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 70
	i32 2810250172, ; 139: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 37
	i32 2819470561, ; 140: System.Xml.dll => 0xa80db4e1 => 21
	i32 2821294376, ; 141: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 70
	i32 2847418871, ; 142: Xamarin.GooglePlayServices.Base => 0xa9b829f7 => 93
	i32 2853208004, ; 143: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 80
	i32 2855708567, ; 144: Xamarin.AndroidX.Transition => 0xaa36a797 => 76
	i32 2903344695, ; 145: System.ComponentModel.Composition => 0xad0d8637 => 112
	i32 2905242038, ; 146: mscorlib.dll => 0xad2a79b6 => 6
	i32 2916838712, ; 147: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 81
	i32 2919462931, ; 148: System.Numerics.Vectors.dll => 0xae037813 => 18
	i32 2921128767, ; 149: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 25
	i32 2955285727, ; 150: GeoTimeTrack.Android => 0xb02614df => 0
	i32 2978675010, ; 151: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 43
	i32 2996846495, ; 152: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 55
	i32 3016983068, ; 153: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 73
	i32 3017076677, ; 154: Xamarin.GooglePlayServices.Maps => 0xb3d4efc5 => 95
	i32 3024354802, ; 155: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 50
	i32 3044182254, ; 156: FormsViewGroup => 0xb57288ee => 2
	i32 3057625584, ; 157: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 63
	i32 3058099980, ; 158: Xamarin.GooglePlayServices.Tasks => 0xb646e70c => 96
	i32 3111772706, ; 159: System.Runtime.Serialization => 0xb979e222 => 1
	i32 3204380047, ; 160: System.Data.dll => 0xbefef58f => 106
	i32 3211777861, ; 161: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 42
	i32 3230466174, ; 162: Xamarin.GooglePlayServices.Basement.dll => 0xc08d007e => 94
	i32 3247949154, ; 163: Mono.Security => 0xc197c562 => 114
	i32 3258312781, ; 164: Xamarin.AndroidX.CardView => 0xc235e84d => 32
	i32 3267021929, ; 165: Xamarin.AndroidX.AsyncLayoutInflater => 0xc2bacc69 => 30
	i32 3286872994, ; 166: SQLite-net.dll => 0xc3e9b3a2 => 7
	i32 3317135071, ; 167: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 41
	i32 3317144872, ; 168: System.Data => 0xc5b79d28 => 106
	i32 3340431453, ; 169: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 29
	i32 3345895724, ; 170: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 68
	i32 3346324047, ; 171: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 64
	i32 3353484488, ; 172: Xamarin.AndroidX.Legacy.Support.Core.UI.dll => 0xc7e21cc8 => 49
	i32 3360279109, ; 173: SQLitePCLRaw.core => 0xc849ca45 => 9
	i32 3362522851, ; 174: Xamarin.AndroidX.Core => 0xc86c06e3 => 39
	i32 3366347497, ; 175: Java.Interop => 0xc8a662e9 => 4
	i32 3374999561, ; 176: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 69
	i32 3395150330, ; 177: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 19
	i32 3404865022, ; 178: System.ServiceModel.Internals => 0xcaf21dfe => 104
	i32 3429136800, ; 179: System.Xml => 0xcc6479a0 => 21
	i32 3430777524, ; 180: netstandard => 0xcc7d82b4 => 105
	i32 3441283291, ; 181: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 44
	i32 3459516321, ; 182: Xamarin.Forms.GoogleMaps => 0xce3407a1 => 85
	i32 3476120550, ; 183: Mono.Android => 0xcf3163e6 => 5
	i32 3486566296, ; 184: System.Transactions => 0xcfd0c798 => 107
	i32 3493954962, ; 185: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 34
	i32 3501239056, ; 186: Xamarin.AndroidX.AsyncLayoutInflater.dll => 0xd0b0ab10 => 30
	i32 3502435540, ; 187: GeoTimeTrack.Android.dll => 0xd0c2ecd4 => 0
	i32 3509114376, ; 188: System.Xml.Linq => 0xd128d608 => 22
	i32 3536029504, ; 189: Xamarin.Forms.Platform.Android.dll => 0xd2c38740 => 88
	i32 3567349600, ; 190: System.ComponentModel.Composition.dll => 0xd4a16f60 => 112
	i32 3618140916, ; 191: Xamarin.AndroidX.Preference => 0xd7a872f4 => 66
	i32 3627220390, ; 192: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 67
	i32 3632359727, ; 193: Xamarin.Forms.Platform => 0xd881692f => 89
	i32 3633644679, ; 194: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 25
	i32 3641597786, ; 195: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 53
	i32 3672681054, ; 196: Mono.Android.dll => 0xdae8aa5e => 5
	i32 3676310014, ; 197: System.Web.Services.dll => 0xdb2009fe => 113
	i32 3682565725, ; 198: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 31
	i32 3684561358, ; 199: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 34
	i32 3689375977, ; 200: System.Drawing.Common => 0xdbe768e9 => 109
	i32 3706696989, ; 201: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 38
	i32 3718780102, ; 202: Xamarin.AndroidX.Annotation => 0xdda814c6 => 24
	i32 3724971120, ; 203: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 63
	i32 3754567612, ; 204: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 11
	i32 3758932259, ; 205: Xamarin.AndroidX.Legacy.Support.V4 => 0xe00cc123 => 51
	i32 3786282454, ; 206: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 33
	i32 3822602673, ; 207: Xamarin.AndroidX.Media => 0xe3d849b1 => 61
	i32 3829621856, ; 208: System.Numerics.dll => 0xe4436460 => 17
	i32 3876362041, ; 209: SQLite-net => 0xe70c9739 => 7
	i32 3885922214, ; 210: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 76
	i32 3888767677, ; 211: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 68
	i32 3896760992, ; 212: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 39
	i32 3920810846, ; 213: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 111
	i32 3921031405, ; 214: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 79
	i32 3931092270, ; 215: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 65
	i32 3945713374, ; 216: System.Data.DataSetExtensions.dll => 0xeb2ecede => 108
	i32 3955647286, ; 217: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 27
	i32 3957819070, ; 218: GeoTimeTrack.dll => 0xebe786be => 3
	i32 3959773229, ; 219: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 55
	i32 3970018735, ; 220: Xamarin.GooglePlayServices.Tasks.dll => 0xeca1adaf => 96
	i32 4025784931, ; 221: System.Memory => 0xeff49a63 => 15
	i32 4101593132, ; 222: Xamarin.AndroidX.Emoji2 => 0xf479582c => 45
	i32 4105002889, ; 223: Mono.Security.dll => 0xf4ad5f89 => 114
	i32 4151237749, ; 224: System.Core => 0xf76edc75 => 13
	i32 4182413190, ; 225: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 58
	i32 4256097574, ; 226: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 38
	i32 4260525087, ; 227: System.Buffers => 0xfdf2741f => 12
	i32 4278134329, ; 228: Xamarin.GooglePlayServices.Maps.dll => 0xfeff2639 => 95
	i32 4292120959 ; 229: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 58
], align 4
@assembly_image_cache_indices = local_unnamed_addr constant [230 x i32] [
	i32 56, i32 92, i32 83, i32 72, i32 72, i32 100, i32 33, i32 74, ; 0..7
	i32 31, i32 86, i32 50, i32 113, i32 36, i32 54, i32 48, i32 23, ; 8..15
	i32 87, i32 17, i32 52, i32 10, i32 15, i32 35, i32 82, i32 47, ; 16..23
	i32 6, i32 14, i32 48, i32 60, i32 107, i32 100, i32 111, i32 41, ; 24..31
	i32 46, i32 79, i32 28, i32 22, i32 102, i32 97, i32 101, i32 8, ; 32..39
	i32 110, i32 109, i32 67, i32 92, i32 101, i32 52, i32 2, i32 71, ; 40..47
	i32 27, i32 89, i32 57, i32 99, i32 14, i32 77, i32 64, i32 28, ; 48..55
	i32 73, i32 78, i32 102, i32 9, i32 43, i32 85, i32 104, i32 84, ; 56..63
	i32 71, i32 61, i32 37, i32 19, i32 90, i32 110, i32 26, i32 87, ; 64..71
	i32 42, i32 1, i32 59, i32 81, i32 46, i32 40, i32 16, i32 20, ; 72..79
	i32 75, i32 91, i32 36, i32 98, i32 10, i32 32, i32 74, i32 13, ; 80..87
	i32 47, i32 59, i32 99, i32 91, i32 65, i32 82, i32 90, i32 86, ; 88..95
	i32 29, i32 94, i32 62, i32 98, i32 12, i32 57, i32 53, i32 20, ; 96..103
	i32 18, i32 49, i32 11, i32 88, i32 93, i32 103, i32 16, i32 77, ; 104..111
	i32 3, i32 60, i32 62, i32 51, i32 69, i32 24, i32 45, i32 66, ; 112..119
	i32 84, i32 8, i32 35, i32 105, i32 4, i32 108, i32 56, i32 78, ; 120..127
	i32 103, i32 40, i32 44, i32 54, i32 75, i32 23, i32 26, i32 83, ; 128..135
	i32 97, i32 80, i32 70, i32 37, i32 21, i32 70, i32 93, i32 80, ; 136..143
	i32 76, i32 112, i32 6, i32 81, i32 18, i32 25, i32 0, i32 43, ; 144..151
	i32 55, i32 73, i32 95, i32 50, i32 2, i32 63, i32 96, i32 1, ; 152..159
	i32 106, i32 42, i32 94, i32 114, i32 32, i32 30, i32 7, i32 41, ; 160..167
	i32 106, i32 29, i32 68, i32 64, i32 49, i32 9, i32 39, i32 4, ; 168..175
	i32 69, i32 19, i32 104, i32 21, i32 105, i32 44, i32 85, i32 5, ; 176..183
	i32 107, i32 34, i32 30, i32 0, i32 22, i32 88, i32 112, i32 66, ; 184..191
	i32 67, i32 89, i32 25, i32 53, i32 5, i32 113, i32 31, i32 34, ; 192..199
	i32 109, i32 38, i32 24, i32 63, i32 11, i32 51, i32 33, i32 61, ; 200..207
	i32 17, i32 7, i32 76, i32 68, i32 39, i32 111, i32 79, i32 65, ; 208..215
	i32 108, i32 27, i32 3, i32 55, i32 96, i32 15, i32 45, i32 114, ; 216..223
	i32 13, i32 58, i32 38, i32 12, i32 95, i32 58 ; 224..229
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
!3 = !{!"Xamarin.Android remotes/origin/d17-5 @ a200af12c1e846626b8caebd926ac14c185f71ec"}
!llvm.linker.options = !{}
