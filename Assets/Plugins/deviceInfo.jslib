mergeInto(LibraryManager.library, 
{


  IsDesktopDeviceInfo: function () {
    var device = ysdk.deviceInfo.isDesktop();
    return device;
  },


});