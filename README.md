# Simple Microsoft MVVM Toolkit Sample

POC minimal yet plausable MVVM app using Microsoft's MVVM Toolkit.

Description: 

This project incorporates features that compensate for some of the limitations of Microsoft's MVVM Toolkit. The demo app is simple enough to be digestable yet complicated enough to be representative of real world MVVM usage.

Ancillary to the evaluation MVVM Toolkit usage, this project also attempts to share code between WinUI 3 and UWP in a manner that minimize the differences (WIP).

## Credits
* MVVM and Toolkit ideas: https://github.com/windows-toolkit/MVVM-Samples , https://github.com/XamlBrewer/UWP-MVVM-Toolkit-Sample , https://github.com/veler/PaZword

## Screenshots
![Screenshot](https://github.com/Noemata/SimpleMVVM/blob/master/HomeView.png)

![Screenshot](https://github.com/Noemata/SimpleMVVM/blob/master/ListView.png)

![Screenshot](https://github.com/Noemata/SimpleMVVM/blob/master/WinUI.png)

## Notes

What limitations?

Persently, when you create a new ViewModel, you have to remember to wire it into your Ioc.  Most MVVM frameworks have this limitation.

A simple solution to this little detail is to use an attribute as shown below:

```
    [RegisterWithIoc(InstanceMode.Transient)]
    public class AboutViewModel : ObservableRecipient
```

This can be incorporated automatically via a project template whenever you generate a ViewModel class.

Have a look at App.xaml.cs to see the details of how this approach works.

Another missing capability from MVVM frameworks in general is the ability to send control events to your ViewModel.  For the ShellViewModel in particular, there is no need to adhere to a strict MVVM approach.

Having EventToCommandBehavior allows you to work around issues such as the one shown below:

```
        <winui:NavigationView x:Name="NavigationView"
                              Header="{x:Bind ViewModel.Header, Mode=OneWay}"
                              IsBackButtonVisible="Collapsed"
                              Background="Transparent"
                              PaneDisplayMode="LeftCompact"
                              IsSettingsVisible="False">
            <i:Interaction.Behaviors>
                <ui:EventToCommandBehavior 
                            Event="ItemInvoked"
                            Command="{x:Bind ViewModel.ItemInvokedCommand}" 
                            PassArguments="true"/>
            </i:Interaction.Behaviors>

            ...

```

Navigation events and other like control events can be dispatched within the ShellViewModel to coordinate with other executive level control operations.  Think of your ShellViewModel as your UI escape hatch where anything goes allowing the rest of your MVVM design to stay clean and sober.

Please let me know if you see ways to improve this sample or if you have a difference of opinion with the approach taken.

