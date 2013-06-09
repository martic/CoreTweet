

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=.

include $(top_srcdir)/config.make

ifeq ($(CONFIG),DEBUG)
ASSEMBLY_COMPILER_COMMAND = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:0 -optimize+ -debug "-define:DEBUG;"
ASSEMBLY = bin/Debug/CoreTweet.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Debug

CORETWEET_DLL_MDB_SOURCE=bin/Debug/CoreTweet.dll.mdb
CORETWEET_DLL_MDB=$(BUILD_DIR)/CoreTweet.dll.mdb
MAKEDOC_SOURCE=makedoc

endif

ifeq ($(CONFIG),RELEASE)
ASSEMBLY_COMPILER_COMMAND = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:0 -optimize+
ASSEMBLY = ../Binary/Nightly/CoreTweet.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = ../Binary/Nightly

CORETWEET_DLL_MDB=
MAKEDOC_SOURCE=makedoc

endif

AL=al
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(CORETWEET_DLL_MDB) \
	$(MAKEDOC)  

LINUX_PKGCONFIG = \
	$(CORETWEET_PC)  


RESGEN=resgen2

MAKEDOC = $(BUILD_DIR)/makedoc
CORETWEET_PC = $(BUILD_DIR)/coretweet.pc

FILES = \
	AssemblyInfo.cs \
	Objects/CoreBase.cs \
	Objects/Entity.cs \
	Objects/Places.cs \
	Objects/Status.cs \
	Objects/User.cs \
	Objects/Tokens.cs \
	Lib/DynamicJson.cs \
	Tiny.cs \
	Objects/Setting.cs \
	Objects/Embed.cs \
	Apis/Lists.cs \
	Objects/Cursored.cs \
	Apis/Rest/Account.cs \
	Apis/Rest/Blocks.cs \
	Apis/Rest/DirectMessages.cs \
	Apis/Rest/Favorites.cs \
	Apis/Rest/Followers.cs \
	Apis/Rest/Friends.cs \
	Apis/Rest/Friendships.cs \
	Apis/Rest/Statuses.cs \
	Apis/Rest/Geo.cs \
	Objects/Helps.cs \
	Apis/Rest/Help.cs \
	Core.cs \
	Apis/Rest/Search.cs \
	Apis/Rest/SavedSearches.cs \
	Apis/Rest/Users.cs \
	Objects/List.cs \
	Objects/SearchQuery.cs \
	Ex/StatusExtension.cs \
	Ex/UserExtension.cs \
	Ex/OtherExtension.cs \
	Apis/Rest/Trends.cs \
	Ex/SearchExtensions.cs \
	Lib/AliceDoll.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	makedoc \
	coretweet.pc.in 

REFERENCES =  \
	System \
	System.Runtime.Serialization \
	System.Xml \
	System.Xml.Linq \
	Microsoft.CSharp \
	System.Data.Linq \
	System.Net \
	System.Core

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

#Targets
all-local: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG)  $(top_srcdir)/config.make



$(eval $(call emit-deploy-target,MAKEDOC))
$(eval $(call emit-deploy-wrapper,CORETWEET_PC,coretweet.pc))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'


$(ASSEMBLY_MDB): $(ASSEMBLY)
$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	make pre-all-local-hook prefix=$(prefix)
	mkdir -p $(shell dirname $(ASSEMBLY))
	make $(CONFIG)_BeforeBuild
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
	make $(CONFIG)_AfterBuild
	make post-all-local-hook prefix=$(prefix)

install-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-install-local-hook prefix=$(prefix)
	make install-satellite-assemblies prefix=$(prefix)
	mkdir -p '$(DESTDIR)$(libdir)/$(PACKAGE)'
	$(call cp,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(CORETWEET_DLL_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(MAKEDOC),$(DESTDIR)$(libdir)/$(PACKAGE))
	mkdir -p '$(DESTDIR)$(libdir)/pkgconfig'
	$(call cp,$(CORETWEET_PC),$(DESTDIR)$(libdir)/pkgconfig)
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-uninstall-local-hook prefix=$(prefix)
	make uninstall-satellite-assemblies prefix=$(prefix)
	$(call rm,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(CORETWEET_DLL_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(MAKEDOC),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(CORETWEET_PC),$(DESTDIR)$(libdir)/pkgconfig)
	make post-uninstall-local-hook prefix=$(prefix)
