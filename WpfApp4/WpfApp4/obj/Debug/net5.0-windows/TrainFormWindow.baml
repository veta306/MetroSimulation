   M S B A M L   `   `   `  ����B  >WpfApp4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp4WpfApp4  5   	       WpfApp4.TrainFormWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp4     5   	     � Title$  TrainFormWindow��5   	   $	��270��6!   $	��370��6.   $	��370��6:   $	��270��6I   $	��370��6Y   $	��270��6h   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��Line:��6-   $��Center=�6=   $
��Left=�6X   �� 5   
   -   5         ��Name$ cmbLine��5   0   $��0��6   $��1��6    $��5q�6A   $��Center=�6L   $	��155��6g   $��Right=�6s    �� SelectedValuePath$ Content��6�   .@�5      �� $	��Red��6   �� 5      $��Green��6   5      �� 5   
   $��1��6   $��0��6   $��Initial Station:��6-   $��Center=�6H   �� 5    
   -   5        $ cmbInitialStation��5    0   $��1��6   $��1��6    $��5q�6K   $��Center=�6V   $	��155��6q   $��Right=�6}   $ Content��6�   .@�5!      �� $��1��6   �� 5"      $��2��6   �� 5#      $��3��6   �� 5$      $��4��6   �� 5%      $��5��6   5&      �� 5'   
   $��2��6   $��0��6   $��
Direction:��6-   $��Center=�6B   $
��Left=�6]   �� 5(   
   -   5        $ cmbDirection��5(   0   $��2��6   $��1��6    $��5q�6F   $��Center=�6Q   $	��155��6l   $��Right=�6x   $ Content��6�   .@�5)      �� $��Forward��6   �� 5*      $��Reverse��6   5+      �� 5-   
   $��3��6   $��0��6   $ ��Stop Duration (From - To):��6-   $��Center=�6R   $
��Left=�6m   �� 5.   
   $��3��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $��5q�6K   $��Center=�6V   $��Right=�6q   .+�5/      �� -   5         ��Name$ txtStopDurationFrom��5/      $��70��63   $��Right=�6>   �� 50      $��-��6   $	��5,0q�6!   �� 51      -   5        $ txtStopDurationTo��51      $��70��61   $��Right=�6<   52      �� 54   
   $��4��6   $��0��6   $"��Travel Duration (From - To):��6-   $��Center=�6T   $
��Left=�6o   �� 55   
   $��4��6   $��1��6"   $ 
Horizontal=�62   $��5q�6K   $��Center=�6V   $��Right=�6q   .+�56      �� -   5        $ txtTravelDurationFrom��56      $��70��65   �� 57      $��-��6   $	��5,0q�6!   �� 58      -   5        $ txtTravelDurationTo��58      $��70��63   59      �� 5;   
   $��5��6   $��2��6"   $ 
Horizontal=�66   $��Center=�6O   $
��0,10q�6l   .+�5<      �� -   5        $
��Save��5<      $��80��6$   $��5q�6/   6:   �� 5=      -	   5        $��Cancel��5=      $��80��6&   $��5q�61   6<   5>      5?      5@      