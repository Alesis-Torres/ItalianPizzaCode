   M S B A M L   `   `   `  ����G  CItalianPicza, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ItalianPiczaItalianPicza  5      &     ItalianPicza.GUI_ModificarReceta   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      &localclr-namespace:ItalianPicza  5        N� Title$  GUI_ModificarReceta��5
       � 	Resources 5         IconButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius  CircleButtonStyle'  !MaterialDesignThemes.Wpf.PackIcon   Kind��%|  &	        &	 �    �� 5   
   " ��6(   .�5      ��  � ��$ 40��6&   �� 5       � ��$ 40��6'   �� 5       � �� �#231B1B6+   �� 5       � ��
 �����6,   �� 5       � ��$ 1q�60   �� 5       � ��
 �����6+   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5      #��z��5      $ 5��5      ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5!      5"      �� 5$   
   " ��6*   .�5%      ��  � ��$ 40��6&   �� 5&       � ��$ 40��6'   �� 5'       � ��
 �  ��6+   �� 5(       � ��
 ���� 6,   �� 5)       � �� 5*      �� 5+      " ��6&   .R�5,      � .+�5-      A� #��z��6&     5.      $ Close=�66   
�������$��Center=�6V   $��Center=�6s   5/      50      52      53      55      .��58      � .+�59   
   �� $
��Left=�6   $	��720��6/   
���   �$��Center=�6K   $	��230��6f   
�������6r   �� 5:   
   $
��Left=�6   $��78��6/   $��
528,20,0,0q�6;   $	��Top=�6O   $	��493��6g   ���#231B1B �� RadiusX$ 25��6�   	 �� RadiusY$	 25��6�   
���  ��$��5��6�   �� 5;   
   $��Modificar receta��6   $
��Left=�6+   $��59��6F   $��
630,34,0,0q�6R   $	��Top=�6f   $	��370��6~   
������ 
�������$��36(�6�   $��Rockwell Extra Bold)�6�   �� 5<   
   $
��Left=�6   $��60��6+   $��
558,34,0,0q�67   $	��Top=�6K   $��56��6c   $"��/Imagenes/Logos/script 2.png��6n   � 5>   
   -   5        
 �Name$
 panelInsumos��5>      $y�Visible=�6%   $��Center=�6:   $
��1260��6W   $��0,0,0,5q�6d   .+�5?      �� $
��Left=�6   $	��431��63   
���   �$	��Top=�6O   $	��568��6g   ���#231B1B$��243,134,0,0q�6�   �� 5@      -   5        $��	lvInsumos��5@      $��257,138,463,198q�6(   ���#231B1B 5B      ��%  '��       �� 5C      " ��6   .�5D      ��  � ��
 �����67   �� 5F       � ��
 �����67   �� 5H       � ��$ 20(�65   �� 5J       � ��$
 Bold"�67   �� 5L       � ��$ Center=�6G   �� 5N       � ��$
 10,5q�64   5P      5Q      ��5T      �� 5U      " ��6   .�5W      ��  � ��
 ���� 67   �� 5Y       � ��
 �����67    �� Triggers 5Z      P� 5\       P� Property �@J�IsMouseOver P� Value . . �5]   "   ��  � �� �#3f30306?   5_      5a       �� View 5d      �� 5e      .P�5g      �� $�� ��6)    �� Width$	 110��64    �� CellTemplate 5h      �� 5i   "   .R�5j   &   �� $	��100��6F   $	��100��6R   $��Right=�6_   ��6,   �� *imagen+6_   5k   #   5m      �� 5p      $��Nombre��6)   $	 170��69    5q      �� 5r   "   .R�5s   &   �� 
�������$��Center=�5u   )   $��Center=�5v   )    �� TextAlignment$ Center=�5w   )   $
��Wrap=�5x   )   $��24(�5y   )   ��5s   0   �� *nombre+5y   )   5z   #   5|      �� 5~      $
��U.M.��6)   $ 70��67    5      �� 5�   "   .R�5�   &   �� 
�������$��Center=�5�   )   $��Center=�5�   )   $��24(�5�   )   ��5�   0   �� *medida+5�   )   5�   #   5�      �� 5�      $��Cantidad��6)   $	 120��6;    5�      �� 5�   "   .R�5�   &   ��  ��Name$ cuadroTextoCantidad��6.   -   5        
���   �
�������$��Center=�5�   )   $��Center=�5�   )   $��24(�5�   )   $	��150��5�   )    �� 	MaxLength$ 3��5�   )    �� TextAlignment$ Center=�5�   )   5�   )   5�   #   5�      �� 5�      $�� ��6)   $ 80��64    5�      �� 5�   "   .R�5�   &   �� -   5        $��40��5�   -   $��40��68   #��[  6D   .��5�   *     $
 Plus=�6B   
�������6N   5�   '   5�   #   5�      5�      5�      �� 5�      $
��Left=�6   $	��387��63   
���   �$	��Top=�6O   $	��404��6g   ���#231B1B$��829,134,0,0q�6�   �� 5�      $��Receta��6   $
��Left=�6%   $��47��6@   $��1015,146,0,0q�6L   $	��Top=�6b   $	��118��6z   $��Britannic Bold)�6�   $��36(�6�   ���#FFFBD46�   �� 5�      -   5        $��lvReceta��5�      $��843,214,27,213q�6'   ���#231B1B 5�      ��%  '��       �� 5�      " ��6   .�5�      ��  � ��
 �����67   �� 5�       � ��
 �����67   �� 5�       � ��$ 20(�65   �� 5�       � ��$
 Bold"�67   �� 5�       � ��$ Center=�6G   �� 5�       � ��$
 10,5q�64   5�      5�      ��5�      �� 5�      " ��6   .�5�      ��  � ��
 ���� 67   �� 5�       � ��
 �����67    5�      P� 5�       �@J�IsMouseOver . . �5�   "   ��  � �� �#3f30306?   5�      5�       5�      �� 5�      .P�5�      �� $��Nombre��6)   $	 150��69    5�      �� 5�   "   .R�5�   &   �� 
�������$��Center=�5�      $��Center=�5�      $��24(�5�      ��5�   0   �� *nombre+5�      5�   #   5�      �� 5�      $��Cantidad��6)   $	 110��6;    5�      �� 5�   "   .R�5�   &   �� 
�������$��Center=�5�   )   $��Center=�5�   )   $��24(�5�   )   ��5�   0   �� *
cantidad+5�   )   5�   #   5�      �� 5�      $
��U.M.��6)   $ 70��67    5�      �� 5�   "   .R�5�   &   �� 
�������$��Center=�5�   )   $��Center=�5�   )   $��24(�5�   )   ��5�   0   �� *medida+5�   )   5�   #   5�      �� 5�      $�� ��6)   $ 60��64    5�      �� 5�   "   .R�5�   &   �� -   5        $��40��5�   -   $��40��68   
���  ��
������ #��[ 6o   ��5�   *   �� 5�   .   " ��6>   .R�5�   2   � .+�5   6   A� #��z��6>     5  6   $ Close=�6N   
�������$��Center=�6n   $��Center=�6�   5  3   5  /   5  '   5  #   5     5
     5     �� 5     -   5        $
��Left=�5     $��63��60   $��856,576,0,0q�6<   $	��Top=�6Q   $	��180��6i   ���#FF0000��5     �� 5     " ��6&   .R�5     �� #��z��6!   $ 20��5     #��z��5     $��0q�5      �� Padding$ 10q�5     ._�5     ��  �� Orientation$ 
Horizontal=�6)   $��Center=�6B   $��Center=�6_   .+�5  "   �� $��Cancelar��6,   $��Center=�5  %   
�������$
��Bold"�5  %   $��22(�5  %   5     5     5     5     �� 5      -   5        $
��Left=�5      $��63��60   $��1041,576,0,0q�6<   $	��Top=�6R   $	��180��6j   ���#008000��5!     �� 5"     " ��6&   .R�5#     �� #��z��6!   $ 20��5$     #��z��5%     $��0q�5&     $ 10q�5'     ._�5(     �� $ 
Horizontal=�6)   $��Center=�6B   $��Center=�6_   .+�5)  "   �� $��	Siguiente��6,   $��Center=�5*     
�������$
��Bold"�5,     $��22(�5-     5.     5/     50     52     53     � 55  
   -	   5        $
 panelInstrucciones��55     $y�	Collapsed=�6+   $��Center=�6B   $
��1260��6_   $��0,0,0,5q�6l   .+�56     �� $
��Left=�6   $	��442��63   
���   �$	��Top=�6O   $	��882��6g   ���#231B1B$��291,120,0,0q�6�   �� 57     -
   5        $��Center=�57     $��63��62   $��	0,583,0,0q�6>   $	��Top=�6Q   $	��180��6i   ���#FF0000��58     �� 59     " ��6&   .R�5:     �� #��z��6!   $ 20��5;     #��z��5<     $��0q�5=     $ 10q�5>     ._�5?     �� $ 
Horizontal=�6)   $��Center=�6B   $��Center=�6_   .+�5@  "   �� $��Regresar��6,   $��Center=�5A  !   
�������$
��Bold"�5C  !   $��22(�5D  !   5E     5F     5G     5I     �� 5J     -   5        $
��Left=�5J     $��63��60   $��774,583,0,0q�6<   $	��Top=�6Q   $	��279��6i   ���#008000��5K     �� 5L     " ��6&   .R�5M     �� #��z��6!   $ 20��5N     #��z��5O     $��0q�5P     $ 10q�5Q     ._�5R     �� $ 
Horizontal=�6)   $��Center=�6B   $��Center=�6_   .+�5S  "   �� $��Registrar instrucciones��6,   $��Center=�5T  !   
�������$
��Bold"�5V  !   $��22(�5W  !   5X     5Y     5Z     5\     �� 5]     -   5        $��tbInstrucciones��5]     $
��Left=�6-   $��323,186,0,0q�6H    �� TextWrapping$
 Wrap=�6]   $	��Top=�6q   $	��812��6�   $	��352��6�   $��26(�6�   
����������#3f3030$	 150��6�   �� 5^     $��Instrucciones��6   $
��Left=�6,   $��47��6G   $��614,134,0,0q�6S   $	��Top=�6h   $	��236��6�   $��Britannic Bold)�6�   $��36(�6�   ���#FFFBD46�   5_     5a     5b     