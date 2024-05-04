; ModuleID = 'obj/Release/android/marshal_methods.arm64-v8a.ll'
source_filename = "obj/Release/android/marshal_methods.arm64-v8a.ll"
target datalayout = "e-m:e-i8:8:32-i16:16:32-i64:64-i128:128-n32:64-S128"
target triple = "aarch64-unknown-linux-android"


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
@assembly_image_cache = local_unnamed_addr global [0 x %struct.MonoImage*] zeroinitializer, align 8
; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = local_unnamed_addr constant [130 x i64] [
	i64 120698629574877762, ; 0: Mono.Android => 0x1accec39cafe242 => 5
	i64 232391251801502327, ; 1: Xamarin.AndroidX.SavedState.dll => 0x3399e9cbc897277 => 35
	i64 276473666272823710, ; 2: Xamarin.Forms.GoogleMaps => 0x3d63b55abf1099e => 41
	i64 687654259221141486, ; 3: Xamarin.GooglePlayServices.Base => 0x98b09e7c92917ee => 49
	i64 702024105029695270, ; 4: System.Drawing.Common => 0x9be17343c0e7726 => 63
	i64 720058930071658100, ; 5: Xamarin.AndroidX.Legacy.Support.Core.UI => 0x9fe29c82844de74 => 29
	i64 870603111519317375, ; 6: SQLitePCLRaw.lib.e_sqlite3.android => 0xc1500ead2756d7f => 10
	i64 872800313462103108, ; 7: Xamarin.AndroidX.DrawerLayout => 0xc1ccf42c3c21c44 => 27
	i64 940822596282819491, ; 8: System.Transactions => 0xd0e792aa81923a3 => 62
	i64 996343623809489702, ; 9: Xamarin.Forms.Platform => 0xdd3b93f3b63db26 => 45
	i64 1000557547492888992, ; 10: Mono.Security.dll => 0xde2b1c9cba651a0 => 64
	i64 1120440138749646132, ; 11: Xamarin.Google.Android.Material.dll => 0xf8c9a5eae431534 => 47
	i64 1301485588176585670, ; 12: SQLitePCLRaw.core => 0x120fce3f338e43c6 => 9
	i64 1425944114962822056, ; 13: System.Runtime.Serialization.dll => 0x13c9f89e19eaf3a8 => 1
	i64 1518315023656898250, ; 14: SQLitePCLRaw.provider.e_sqlite3 => 0x151223783a354eca => 11
	i64 1624659445732251991, ; 15: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0x168bf32877da9957 => 20
	i64 1795316252682057001, ; 16: Xamarin.AndroidX.AppCompat.dll => 0x18ea3e9eac997529 => 21
	i64 1836611346387731153, ; 17: Xamarin.AndroidX.SavedState => 0x197cf449ebe482d1 => 35
	i64 1981742497975770890, ; 18: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x1b80904d5c241f0a => 32
	i64 2064708342624596306, ; 19: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x1ca7514c5eecb152 => 56
	i64 2262844636196693701, ; 20: Xamarin.AndroidX.DrawerLayout.dll => 0x1f673d352266e6c5 => 27
	i64 2329709569556905518, ; 21: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x2054ca829b447e2e => 31
	i64 2337758774805907496, ; 22: System.Runtime.CompilerServices.Unsafe => 0x207163383edbc828 => 17
	i64 2470498323731680442, ; 23: Xamarin.AndroidX.CoordinatorLayout => 0x2248f922dc398cba => 23
	i64 2547086958574651984, ; 24: Xamarin.AndroidX.Activity.dll => 0x2359121801df4a50 => 19
	i64 2592350477072141967, ; 25: System.Xml.dll => 0x23f9e10627330e8f => 18
	i64 2624866290265602282, ; 26: mscorlib.dll => 0x246d65fbde2db8ea => 6
	i64 2783046991838674048, ; 27: System.Runtime.CompilerServices.Unsafe.dll => 0x269f5e7e6dc37c80 => 17
	i64 2960931600190307745, ; 28: Xamarin.Forms.Core => 0x2917579a49927da1 => 39
	i64 3017704767998173186, ; 29: Xamarin.Google.Android.Material => 0x29e10a7f7d88a002 => 47
	i64 3289520064315143713, ; 30: Xamarin.AndroidX.Lifecycle.Common => 0x2da6b911e3063621 => 30
	i64 3344514922410554693, ; 31: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x2e6a1a9a18463545 => 59
	i64 3411255996856937470, ; 32: Xamarin.GooglePlayServices.Basement => 0x2f5737416a942bfe => 50
	i64 3522470458906976663, ; 33: Xamarin.AndroidX.SwipeRefreshLayout => 0x30e2543832f52197 => 36
	i64 3531994851595924923, ; 34: System.Numerics => 0x31042a9aade235bb => 16
	i64 3727469159507183293, ; 35: Xamarin.AndroidX.RecyclerView => 0x33baa1739ba646bd => 34
	i64 3884844916416404251, ; 36: GeoTimeTrack.dll => 0x35e9bdde627b5b1b => 3
	i64 3936216335706411319, ; 37: Xamarin.Forms.GoogleMaps.dll => 0x36a03fe700ded137 => 41
	i64 4247996603072512073, ; 38: Xamarin.GooglePlayServices.Tasks => 0x3af3ea6755340049 => 52
	i64 4337444564132831293, ; 39: SQLitePCLRaw.batteries_v2.dll => 0x3c31b2d9ae16203d => 8
	i64 4525561845656915374, ; 40: System.ServiceModel.Internals => 0x3ece06856b710dae => 60
	i64 4794310189461587505, ; 41: Xamarin.AndroidX.Activity => 0x4288cfb749e4c631 => 19
	i64 4795410492532947900, ; 42: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0x428cb86f8f9b7bbc => 36
	i64 4856518254040561554, ; 43: GeoTimeTrack.Android.dll => 0x4365d19f0149b792 => 0
	i64 5142919913060024034, ; 44: Xamarin.Forms.Platform.Android.dll => 0x475f52699e39bee2 => 44
	i64 5203618020066742981, ; 45: Xamarin.Essentials => 0x4836f704f0e652c5 => 38
	i64 5256995213548036366, ; 46: Xamarin.Forms.Maps.Android.dll => 0x48f4994b4175a10e => 42
	i64 5507995362134886206, ; 47: System.Core.dll => 0x4c705499688c873e => 13
	i64 6085203216496545422, ; 48: Xamarin.Forms.Platform.dll => 0x5472fc15a9574e8e => 45
	i64 6086316965293125504, ; 49: FormsViewGroup.dll => 0x5476f10882baef80 => 2
	i64 6183170893902868313, ; 50: SQLitePCLRaw.batteries_v2 => 0x55cf092b0c9d6f59 => 8
	i64 6401687960814735282, ; 51: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0x58d75d486341cfb2 => 31
	i64 6548213210057960872, ; 52: Xamarin.AndroidX.CustomView.dll => 0x5adfed387b066da8 => 26
	i64 6659513131007730089, ; 53: Xamarin.AndroidX.Legacy.Support.Core.UI.dll => 0x5c6b57e8b6c3e1a9 => 29
	i64 7141281584637745974, ; 54: Xamarin.GooglePlayServices.Maps.dll => 0x631aedc3dd5f1b36 => 51
	i64 7330419912715392478, ; 55: Xamarin.Forms.GoogleMaps.Android => 0x65bae21287d9d5de => 40
	i64 7635363394907363464, ; 56: Xamarin.Forms.Core.dll => 0x69f6428dc4795888 => 39
	i64 7637365915383206639, ; 57: Xamarin.Essentials.dll => 0x69fd5fd5e61792ef => 38
	i64 7654504624184590948, ; 58: System.Net.Http => 0x6a3a4366801b8264 => 15
	i64 7735352534559001595, ; 59: Xamarin.Kotlin.StdLib.dll => 0x6b597e2582ce8bfb => 55
	i64 7820441508502274321, ; 60: System.Data => 0x6c87ca1e14ff8111 => 61
	i64 7836164640616011524, ; 61: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x6cbfa6390d64d704 => 20
	i64 8083354569033831015, ; 62: Xamarin.AndroidX.Lifecycle.Common.dll => 0x702dd82730cad267 => 30
	i64 8167236081217502503, ; 63: Java.Interop.dll => 0x7157d9f1a9b8fd27 => 4
	i64 8187640529827139739, ; 64: Xamarin.KotlinX.Coroutines.Android => 0x71a057ae90f0109b => 58
	i64 8626175481042262068, ; 65: Java.Interop => 0x77b654e585b55834 => 4
	i64 8853378295825400934, ; 66: Xamarin.Kotlin.StdLib.Common.dll => 0x7add84a720d38466 => 54
	i64 9324707631942237306, ; 67: Xamarin.AndroidX.AppCompat => 0x8168042fd44a7c7a => 21
	i64 9662334977499516867, ; 68: System.Numerics.dll => 0x8617827802b0cfc3 => 16
	i64 9678050649315576968, ; 69: Xamarin.AndroidX.CoordinatorLayout.dll => 0x864f57c9feb18c88 => 23
	i64 9808709177481450983, ; 70: Mono.Android.dll => 0x881f890734e555e7 => 5
	i64 9834056768316610435, ; 71: System.Transactions.dll => 0x8879968718899783 => 62
	i64 9875200773399460291, ; 72: Xamarin.GooglePlayServices.Base.dll => 0x890bc2c8482339c3 => 49
	i64 9998632235833408227, ; 73: Mono.Security => 0x8ac2470b209ebae3 => 64
	i64 10038780035334861115, ; 74: System.Net.Http.dll => 0x8b50e941206af13b => 15
	i64 10226222362177979215, ; 75: Xamarin.Kotlin.StdLib.Jdk7 => 0x8dead70ebbc6434f => 56
	i64 10229024438826829339, ; 76: Xamarin.AndroidX.CustomView => 0x8df4cb880b10061b => 26
	i64 10321854143672141184, ; 77: Xamarin.Jetbrains.Annotations.dll => 0x8f3e97a7f8f8c580 => 53
	i64 10406448008575299332, ; 78: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x906b2153fcb3af04 => 59
	i64 10430153318873392755, ; 79: Xamarin.AndroidX.Core => 0x90bf592ea44f6673 => 24
	i64 10775409704848971057, ; 80: Xamarin.Forms.Maps => 0x9589f20936d1d531 => 43
	i64 10812185687485485027, ; 81: GeoTimeTrack.Android => 0x960c9998ec228fe3 => 0
	i64 11023048688141570732, ; 82: System.Core => 0x98f9bc61168392ac => 13
	i64 11037814507248023548, ; 83: System.Xml => 0x992e31d0412bf7fc => 18
	i64 11162124722117608902, ; 84: Xamarin.AndroidX.ViewPager => 0x9ae7d54b986d05c6 => 37
	i64 11340910727871153756, ; 85: Xamarin.AndroidX.CursorAdapter => 0x9d630238642d465c => 25
	i64 11444370155346000636, ; 86: Xamarin.Forms.Maps.Android => 0x9ed292057b7afafc => 42
	i64 11529969570048099689, ; 87: Xamarin.AndroidX.ViewPager.dll => 0xa002ae3c4dc7c569 => 37
	i64 11739066727115742305, ; 88: SQLite-net.dll => 0xa2e98afdf8575c61 => 7
	i64 11806260347154423189, ; 89: SQLite-net => 0xa3d8433bc5eb5d95 => 7
	i64 12088794037205360587, ; 90: GeoTimeTrack => 0xa7c40620ae7c5fcb => 3
	i64 12102847907131387746, ; 91: System.Buffers => 0xa7f5f40c43256f62 => 12
	i64 12279246230491828964, ; 92: SQLitePCLRaw.provider.e_sqlite3.dll => 0xaa68a5636e0512e4 => 11
	i64 12451044538927396471, ; 93: Xamarin.AndroidX.Fragment.dll => 0xaccaff0a2955b677 => 28
	i64 12466513435562512481, ; 94: Xamarin.AndroidX.Loader.dll => 0xad01f3eb52569061 => 33
	i64 12538491095302438457, ; 95: Xamarin.AndroidX.CardView.dll => 0xae01ab382ae67e39 => 22
	i64 12828192437253469131, ; 96: Xamarin.Kotlin.StdLib.Jdk8.dll => 0xb206e50e14d873cb => 57
	i64 12963446364377008305, ; 97: System.Drawing.Common.dll => 0xb3e769c8fd8548b1 => 63
	i64 13291835053457086558, ; 98: Xamarin.Forms.GoogleMaps.Android.dll => 0xb876158ed665185e => 40
	i64 13370592475155966277, ; 99: System.Runtime.Serialization => 0xb98de304062ea945 => 1
	i64 13454009404024712428, ; 100: Xamarin.Google.Guava.ListenableFuture => 0xbab63e4543a86cec => 48
	i64 13465488254036897740, ; 101: Xamarin.Kotlin.StdLib => 0xbadf06394d106fcc => 55
	i64 13572454107664307259, ; 102: Xamarin.AndroidX.RecyclerView.dll => 0xbc5b0b19d99f543b => 34
	i64 13647894001087880694, ; 103: System.Data.dll => 0xbd670f48cb071df6 => 61
	i64 13828521679616088467, ; 104: Xamarin.Kotlin.StdLib.Common => 0xbfe8c733724e1993 => 54
	i64 13959074834287824816, ; 105: Xamarin.AndroidX.Fragment => 0xc1b8989a7ad20fb0 => 28
	i64 13967638549803255703, ; 106: Xamarin.Forms.Platform.Android => 0xc1d70541e0134797 => 44
	i64 14124974489674258913, ; 107: Xamarin.AndroidX.CardView => 0xc405fd76067d19e1 => 22
	i64 14792063746108907174, ; 108: Xamarin.Google.Guava.ListenableFuture.dll => 0xcd47f79af9c15ea6 => 48
	i64 14852515768018889994, ; 109: Xamarin.AndroidX.CursorAdapter.dll => 0xce1ebc6625a76d0a => 25
	i64 15279429628684179188, ; 110: Xamarin.KotlinX.Coroutines.Android.dll => 0xd40b704b1c4c96f4 => 58
	i64 15370334346939861994, ; 111: Xamarin.AndroidX.Core.dll => 0xd54e65a72c560bea => 24
	i64 15609085926864131306, ; 112: System.dll => 0xd89e9cf3334914ea => 14
	i64 15810740023422282496, ; 113: Xamarin.Forms.Xaml => 0xdb6b08484c22eb00 => 46
	i64 15930129725311349754, ; 114: Xamarin.GooglePlayServices.Tasks.dll => 0xdd1330956f12f3fa => 52
	i64 16154507427712707110, ; 115: System => 0xe03056ea4e39aa26 => 14
	i64 16423015068819898779, ; 116: Xamarin.Kotlin.StdLib.Jdk8 => 0xe3ea453135e5c19b => 57
	i64 16755018182064898362, ; 117: SQLitePCLRaw.core.dll => 0xe885c843c330813a => 9
	i64 16833383113903931215, ; 118: mscorlib => 0xe99c30c1484d7f4f => 6
	i64 17704177640604968747, ; 119: Xamarin.AndroidX.Loader => 0xf5b1dfc36cac272b => 33
	i64 17710060891934109755, ; 120: Xamarin.AndroidX.Lifecycle.ViewModel => 0xf5c6c68c9e45303b => 32
	i64 17816041456001989629, ; 121: Xamarin.Forms.Maps.dll => 0xf73f4b4f90a1bbfd => 43
	i64 17838668724098252521, ; 122: System.Buffers.dll => 0xf78faeb0f5bf3ee9 => 12
	i64 17882897186074144999, ; 123: FormsViewGroup => 0xf82cd03e3ac830e7 => 2
	i64 17891337867145587222, ; 124: Xamarin.Jetbrains.Annotations => 0xf84accff6fb52a16 => 53
	i64 17892495832318972303, ; 125: Xamarin.Forms.Xaml.dll => 0xf84eea293687918f => 46
	i64 17969331831154222830, ; 126: Xamarin.GooglePlayServices.Maps => 0xf95fe418471126ee => 51
	i64 17986907704309214542, ; 127: Xamarin.GooglePlayServices.Basement.dll => 0xf99e554223166d4e => 50
	i64 18129453464017766560, ; 128: System.ServiceModel.Internals.dll => 0xfb98c1df1ec108a0 => 60
	i64 18370042311372477656 ; 129: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0xfeef80274e4094d8 => 10
], align 8
@assembly_image_cache_indices = local_unnamed_addr constant [130 x i32] [
	i32 5, i32 35, i32 41, i32 49, i32 63, i32 29, i32 10, i32 27, ; 0..7
	i32 62, i32 45, i32 64, i32 47, i32 9, i32 1, i32 11, i32 20, ; 8..15
	i32 21, i32 35, i32 32, i32 56, i32 27, i32 31, i32 17, i32 23, ; 16..23
	i32 19, i32 18, i32 6, i32 17, i32 39, i32 47, i32 30, i32 59, ; 24..31
	i32 50, i32 36, i32 16, i32 34, i32 3, i32 41, i32 52, i32 8, ; 32..39
	i32 60, i32 19, i32 36, i32 0, i32 44, i32 38, i32 42, i32 13, ; 40..47
	i32 45, i32 2, i32 8, i32 31, i32 26, i32 29, i32 51, i32 40, ; 48..55
	i32 39, i32 38, i32 15, i32 55, i32 61, i32 20, i32 30, i32 4, ; 56..63
	i32 58, i32 4, i32 54, i32 21, i32 16, i32 23, i32 5, i32 62, ; 64..71
	i32 49, i32 64, i32 15, i32 56, i32 26, i32 53, i32 59, i32 24, ; 72..79
	i32 43, i32 0, i32 13, i32 18, i32 37, i32 25, i32 42, i32 37, ; 80..87
	i32 7, i32 7, i32 3, i32 12, i32 11, i32 28, i32 33, i32 22, ; 88..95
	i32 57, i32 63, i32 40, i32 1, i32 48, i32 55, i32 34, i32 61, ; 96..103
	i32 54, i32 28, i32 44, i32 22, i32 48, i32 25, i32 58, i32 24, ; 104..111
	i32 14, i32 46, i32 52, i32 14, i32 57, i32 9, i32 6, i32 33, ; 112..119
	i32 32, i32 43, i32 12, i32 2, i32 53, i32 46, i32 51, i32 50, ; 120..127
	i32 60, i32 10 ; 128..129
], align 4

@marshal_methods_number_of_classes = local_unnamed_addr constant i32 0, align 4

; marshal_methods_class_cache
@marshal_methods_class_cache = global [0 x %struct.MarshalMethodsManagedClass] [
], align 8; end of 'marshal_methods_class_cache' array


@get_function_pointer = internal unnamed_addr global void (i32, i32, i32, i8**)* null, align 8

; Function attributes: "frame-pointer"="non-leaf" "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+neon,+outline-atomics" uwtable willreturn writeonly
define void @xamarin_app_init (void (i32, i32, i32, i8**)* %fn) local_unnamed_addr #0
{
	store void (i32, i32, i32, i8**)* %fn, void (i32, i32, i32, i8**)** @get_function_pointer, align 8
	ret void
}

; Names of classes in which marshal methods reside
@mm_class_names = local_unnamed_addr constant [0 x i8*] zeroinitializer, align 8
@__MarshalMethodName_name.0 = internal constant [1 x i8] c"\00", align 1

; mm_method_names
@mm_method_names = local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	; 0
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		i8* getelementptr inbounds ([1 x i8], [1 x i8]* @__MarshalMethodName_name.0, i32 0, i32 0); name
	}
], align 8; end of 'mm_method_names' array


attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable willreturn writeonly "frame-pointer"="non-leaf" "target-cpu"="generic" "target-features"="+neon,+outline-atomics" }
attributes #1 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable "frame-pointer"="non-leaf" "target-cpu"="generic" "target-features"="+neon,+outline-atomics" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1, !2, !3, !4, !5}
!llvm.ident = !{!6}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"branch-target-enforcement", i32 0}
!3 = !{i32 1, !"sign-return-address", i32 0}
!4 = !{i32 1, !"sign-return-address-all", i32 0}
!5 = !{i32 1, !"sign-return-address-with-bkey", i32 0}
!6 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
!llvm.linker.options = !{}
