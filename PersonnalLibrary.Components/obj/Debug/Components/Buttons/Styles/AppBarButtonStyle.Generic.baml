   M S B A M L   `   `   `  ����\  XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source   AppBarButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType'    !System.Windows.VisualStateManager    VisualStateGroups%   System.Windows.VisualStateGroup   Name   States    System.Windows.VisualState	   Name
   
Storyboard �� 
TargetName �� TargetProperty g� Duration	 W� To W� EasingFunction.  (System.Windows.Media.Animation.CubicEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode J� RenderTransformOrigin ��Name  Segoe UI Symbol  AppBarRefreshButtonStyle �� BasedOn  AppBarMessageButtonStyle  AppBarAddButtonStyle  AppBarStopButtonStyle  AppBarSettingsButtonStyle  AppBarBackButtonStyle�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5        5      �� 5   
   $[ Upack://application:,,,/PersonnalLibrary;component/Components/Common/Font.Generic.xaml?�6   %{  &	        7  7  7  &	 =    7   &	 �    7   &	 �    7   &	 '    7   &	 u    7   &	 �    7   �� 5	      " ��6&   .�5
   
   ��  � ��
 �   �6'   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      � $��60��6    5        5      $ CommonStates��6/   . 5   "     $	 Normal��6.     5   "   $	 Pressed��6.   .
 5   &   �� .��5   *   W� $ scaleTransform��5   -   $ ScaleX�5   -   $ 0:0:0.3D�5   -   $
 0.90X�5   -    5   .     5   2   $ EaseOut=�6<   5   +   W� 5   *   $ scaleTransform��5   -   $ ScaleY�5    -   $ 0:0:0.3D�5!   -   $
 0.90X�5"   -    5#   .     5$   2   $ EaseOut=�6<   5&   +   5'   '   5(   #     5)   "   $	 Disabled��6.     5*   "   $	 	MouseOver��6.   5+        5,      $ FocusStates��6/   . 5-   "     $	 	Unfocused��6.     5.   "   $	 Focused��6.   5/   #   50      $ 0.5, 0.5&�53   Z   z�54      �� 55      scaleTransform��6-   .+�58      �� $ PART_CircleOut��6$   8��  $	��⭕��6e   $��
0,-8, 0, 0q�6u   $��75(�6�   $��Center=�6�   $��Center=�6�   �� 59      $ PART_Circle��6$   8�� $	��●��6b   $��0, -8, 0, 0q�6r   $��64(�6�   $��Center=�6�   $��Center=�6�   
������ 6�   �� 5:      $ PART_Content��6$   8�� #��z��6c   $��30(�6�   $��Center=�6�   $��Center=�6�   5;      5<      5>      5?      �� 5B      " ��6   8   .�5C   
   ��  � ��$	 ��6$   5D      �� 5G      " ��6   8   .�5H   
   ��  � ��$	 ��6$   5I      �� 5L      " ��6   8   .�5M   
   ��  � ��$	 ��6$   5N      �� 5Q      " ��6   8   .�5R   
   ��  � ��$	 ��6$   5S      �� 5V      " ��6   8   .�5W   
   ��  � ��$	 ��6$   5X      �� 5[      " ��6   8   .�5\   
   ��  � ��$	 ��6$   5]      5_      