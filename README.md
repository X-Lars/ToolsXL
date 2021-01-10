# ToolsXL

## Config

Provides a way to store custom classes into the the App.config file and access the properties in an intuitive way.
If your custom class implements the INotifyPropertyChanged interface changes made to properties are automatically saved to the App.config file, else the custom class is saved when the program exits. Supports most CLR types and enums, could be easily extended if you wish.


### Examples

Any call to the static Config<T> class saves an instance of T into the App.config.

    Config<ExampleConfigAuto>.Print();

Creates an auto save enabled, eg. ExampleConfigAuto implements INotifyPropertyChanged, configuration with specified values. Get and set properties have equal functionality, the set property is only implemented for code clarity. Both can be used to get and set property values.

    var enumValue = Config<ExampleConfigAuto>.Get.ExampleEnum;
    var intValue = Config<ExampleConfigAuto>.Set.ExampleInt = 3;
    Config<ExampleConfigAuto>.Set.ExampleString = "Name";
    Config<ExampleConfigAuto>.Get.ExampleBool = true;

When creating a configuration or any other class manually, that doesn't implements INotifyPropertyChanged, use the Config<T>.Save(T) method.

    var manualConfig = new ExampleConfigManual();
    manualConfig.ID = 7;
    manualConfig.Name = "Just a name";

    // Save the manually created config
    Config<ExampleConfigManual>.Save(manualConfig);

    // Modifies a property, but it is not saved since ExampleConfigManual doesn't implement INotifyPropertyChanged
    Config<ExampleConfigManual>.Set.Name = "Modified Name";

    // Save the configuration
    Config<ExampleConfigManual>.Save();

Modifying a property using SetProperty() will save the property even if the configuration class doesn't implement INotifyPropertyChanged.

    Config<ExampleConfigManual>.SetProperty("Name", "Another modification");

If the program closes normally, the configuration is automatically saved and you don't have to call the Save() method. But just to be sure, use the Save() method after you made your changes or use the SetProperty() method if you don't want to implement INotifyPropertyChanged, if the program closes in an abnormal way your changes are lost.

The Config.cs file contains all functional code, just copy and paste in your own project if you like.


## Status

Provides a wrapper for bitwise operations on a status flag or any other enumeration.

### Examples

    // Define the enumeration to use
    [Flags]
    enum StatusFlags
    {
        DEVICE_READY = 0,
        DEVICE_ERROR = 1,
        DEVICE_BUSY  = 2,
        DEVICE_INIT  = 4
    }
    
    // Create the Status class
    Status<StatusFlags> deviceStatus = new Status<StatusFlags>(StatusFlags.DEVICE_INIT);
    
    // Override the status flags
    deviceStatus = StatusFlags.DEVICE_READY;
    
    // Assign the Status class to a StatusFlags enumeration
    StatusFlags flags;
    flags = deviceStatus;
    
    // Set a flag
    deviceStatus += StatusFlags.DEVICE_BUSY;
    
    // Check if a flag is set
    if(deviceStatus == StatusFlags.DEVICE_ERROR)
    {
        // Do something...
    }
    
    // Check if a flag is not set
    if(deviceStatus != StatusFlags.DEVICE_READY)
    {
        // Do something else...
    }
    
    // Clear a flag
    deviceStatus -= StatusFlags.DEVICE_ERROR
    
The Status.cs file contains all functional code, just copy and paste in your own project if you like.
 
