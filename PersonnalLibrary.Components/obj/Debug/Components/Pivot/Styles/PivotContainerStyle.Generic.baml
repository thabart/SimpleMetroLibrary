   M S B A M L   `   `   `  ����K  GPersonnalLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:PersonnalLibrary.ComponentsPersonnalLibrary.Components  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType   ContextBarButtons �� Property �� Value  ContentPivotContainer  	MenuTitle  MenuTitleControlTemplate �� 
TargetType �� Triggers P� Property P� Value0    *PersonnalLibrary.Components.PivotContainer �Name'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup	   Name
   States   System.Windows.VisualState   Name   
Storyboard �� 
TargetName g� Duration �� TargetProperty	 W� To � RowDefinitions ��Name �� 	IsDefault �� Orientation T� X T� Y �� ElementName �� Path�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      3local)clr-namespace:PersonnalLibrary.Components  5      %�  &	        &	 N     &	 �     &	 4    '       7  7  7  7   �� 5      "  �6   .�5   
   ��  � �� �#5911016'   5      �� 5      "  ��6   .�5   
   ��  � ��
 ���� 6'   5      �� 5      "  ��6   .�5   
   ��  � ��$ 20(�6%   �� 5   
    � �� �#DCCBCC6'   �� 5   
    � ��$ Segoe UI)�6'   5      �� 5      " ��67   .R�5   
   �� #��z��6   #��z��5       #��z��5       #��z��5       6L    5   
   P� 5       �@��	IsDefault . . �5      ��  � ��
 �   �6/   5      5!      �� 5#      "    6   .�5$   
   ��  � ��$ Stretch=�6.   �� 5%   
    � ��$ Stretch=�60   �� 5&   
    � �� 5'      �� 5(      "   6"   .R�5)      � $ PART_GlobalContainer��6    5+        5,      $	 Pivot��6/   .
 5-   "     $ Inactive��6.     5.   "   $ Active��6.   . 5/   &   �� .��50   *   W� $ TranslationFunctionFooter��6:   $ 0:0:0.3D�51   :   $ Y�52   :   $ 0X�53   :   54   '   55   #   56       59      �� 5:      $
��Auto �6,   �� 5;      $��* �6,   �� 5<      $
��Auto �6,   .+�5@      �� $ PART_NavigationBar��6c   $��0��6V   ��6'   �� *NavigationMenuElements+��5A      �� 5B   "   .R�5C   &   �� 8��  8�� $��5q�6   6-   �� *	IsCurrent+��6M   �� *Title+��6h   �� *NavigateCmd+��6�   �� *PivotElement+6  5D   #   ��5F      �� 5G   "   .R�5H   &   �� $ 
Horizontal=�61   5I   #   z�5K      T� 5L   "   $ 0Q�65   5N      �� 5Q      $ PART_ElementsContainer��6�   $��Stretch=�6'   $��1��6v   $��5q�6�   ��6C   �� *PivotController.PivotItems+��5R      �� 5S   "   .R�5T   &   �� $ 
Horizontal=�61   8�� 5U   '   5V   #   z�5X      T� 5Y   "   $ 0Q�65   5[      � 5^      8�� $��2��6J   .+�5_      �� $ PART_FooterContainer��6t   ��6+   �� *20PivotController.SelectedPivotItem.ContextButtons+��5`   "   �� 5a   &   .R�5b   *   �� $ 
Horizontal=�65   5c   '   z�5e   "   T� 5f   &   TranslationFunctionFooter�� 6\   �� $ PART_FooterContainer��$ ActualHeight�5h      5i      5j      5k      5m      5n      5p      