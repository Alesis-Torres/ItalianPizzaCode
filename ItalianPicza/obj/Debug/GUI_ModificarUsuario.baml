   M S B A M L   `   `   `  ����G  CItalianPicza, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ItalianPiczaItalianPicza  5      '    !ItalianPicza.GUI_ModificarUsuario   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      &localclr-namespace:ItalianPicza  5        N� Title$  GUI_ModificarUsuario��5
      .��5      � .+�5   
   �� $
��Left=�6   $	��720��6/   
���   �$��Center=�6K   $	��230��6f   
�������6r   �� 5   
   $
��Left=�6   $��78��6/   $��
478,20,0,0q�6;   $	��Top=�6O   $	��541��6g   ���#231B1B �� RadiusX$ 25��6�    �� RadiusY$ 25��6�   
���  ��$��5��6�   �� 5   
   $��Modificar usuario��6   $
��Left=�6,   $��59��6G   $��
598,35,0,0q�6S   $	��Top=�6g   $	��400��6   
������ 
�������$��36(�6�   $��Rockwell Extra Bold)�6�   �� 5   
   $
��Left=�6   $��70��6+   $��
503,24,0,0q�67   $	��Top=�6K   $��81��6c   $ ��Imagenes/Logos/users 3.png��6n   �� 5   
   $
��Left=�6   $	��541��6/   $��285,119,0,0q�6<   
���   �$	��Top=�6`   $	��954��6x   
�������6�   �� 5   
   $��Nombre:��6   $
��Left=�6"   $��32��6=   $��423,144,0,0q�6I   $	��Top=�6^   $	��113��6v   $��22(�6�   $��Georgia)�6�   $
��Bold"�6�   �� 5   
   $��Apellido Paterno:��6   $
��Left=�6,   $��38��6G   $��325,208,0,0q�6S   $	��Top=�6h   $	��211��6�   $��22(�6�   $��Georgia)�6�   $
��Bold"�6�   �� 5   
   $��Apellido Materno:��6   $
��Left=�6,   $��38��6G   $��318,274,0,0q�6S   $	��Top=�6h   $	��218��6�   $��22(�6�   $��Georgia)�6�   $
��Bold"�6�   �� 5   
   $��Nombre de Usuario:��6   $
��Left=�6-   $��38��6H   $��	294,0,0,0q�6T   $��Center=�6g   $	��244��6�   $��22(�6�   $��Georgia)�6�   $
��Bold"�6�   �� 5   
   $
��Rol:��6   $
��Left=�6   $��38��6:   $��477,400,0,0q�6F   $	��Top=�6[   $��52��6s   $��22(�6~   $��Georgia)�6�   $
��Bold"�6�   �� 5   
   $��
Teléfono:��6   $
��Left=�6$   $��38��6?   $��416,468,0,0q�6K   $	��Top=�6`   $	��113��6x   $��22(�6�   $��Georgia)�6�   $
��Bold"�6�   �� 5   
   -   5        $��cuadroTextoNombre��5      $
��Left=�6+   $��55��6F   $��553,132,0,0q�6R    �� TextWrapping$
 Wrap=�6g   $��20(�6{   $	��Top=�6�   $	��284��6�    !  MaterialDesignOutlinedTextBox#��[  6�   $��5,0,5,0q�6�   .��6�   )  #MaterialDesignThemes.Wpf.HintAssist   Hint 5      �� 5      $��Nombre��6   
�������$��18(�6<   .��5   .   5      �� 5   
   -   5        $ ��cuadroTextoApellidoPaterno��5      $
��Left=�64   $��55��6O   $��553,200,0,0q�6[   $
 Wrap=�6p   $��20(�6�   $	��Top=�6�   $	��284��6�   #��[  6�   $��5,0,5,0q�6�   .��6�    5       �� 5!      $��Apellido paterno��6   
�������$��18(�6F   .��5"   .   5#      �� 5$   
   -   5        $ ��cuadroTextoApellidoMaterno��5$      $
��Left=�64   $��55��6O   $��553,266,0,0q�6[   $
 Wrap=�6p   $��20(�6�   $	��Top=�6�   $	��284��6�   #��[  6�   $��5,0,5,0q�6�   .��6�    5%      �� 5&      $��Apellido materno��6   
�������$��18(�6F   .��5'   .   5(      �� 5)   
   -   5        $��cuadroTextoNombreUsuario��5)      $
��Left=�62   $��55��6M   $��553,334,0,0q�6Y   $
 Wrap=�6n   $��20(�6�   $	��Top=�6�   $	��284��6�   #��[  6�   $��5,0,5,0q�6�   .��6�    5*      �� 5+      $��Nombre usuario��6   
�������$��18(�6D   .��5,   .   5-      �� 5.   
   -   5        $��cuadroTextoTelefono��5.      $
��Left=�6-   $��55��6H    �� 	MaxLength$ 10��6T   $��553,457,0,0q�6c   $
 Wrap=�6x   $��20(�6�   $	��Top=�6�   $	��284��6�   #��[  6�   $��5,0,5,0q�6�   .��6   5/      �� 50      $��Telefono��6   
�������$��18(�6>   .��51   .   52      �� 53   
   -   5        $��cuadroTextoCorreo��53      $
��Left=�6+   $��54��6F   $��553,534,0,0q�6R   $
 Wrap=�6g   $��20(�6{   $	��Top=�6�   $	��284��6�   #��[  6�   $��5,0,5,0q�6�   .��6�    54      �� 55      $��Email��6   
�������$��18(�6;   .��56   .   57      �� 58   
   -   5        $��cbRol��58      $
��Left=�6    $��38��6;   $��553,400,0,0q�6G   $	��Top=�6\   $	��284��6t   $��16(�6�    " MaterialDesignOutlinedComboBox#��[ 6�   $��5,0,5,0q�6�   .@�59      �� $��Gerente de sucursal��6   �� 5:      $��Cocinero��6   �� 5;      $��Mesero��6   5<      �� 5=   
   -   5        $��imagenUsuario��5=      $
��Left=�6%   $	��242��6@   $��920,200,0,0q�6M   $	��Top=�6b   $	��260��6z   $&�� Imagenes/Indicadores/Group 1.png��6�   �� 5>   
   -	   5        $	��200��5>      $��50��5?   	   ���#00008B
�������$
��Bold"�5B   	     MaterialDesignFlatButton#��[ 5C   	   ���#00008B$��941,462,139,208q�5D      ��5E      �� 5F       �� 
TargetType" ��6"   .R�5G      �� #��z��6    �� CornerRadius$ 20��5H      #��z��5I      $��0q�5J       �� Padding$ 10q�5K      ._�5L      �� 	 �� Orientation$	 
Horizontal=�6%   $��Center=�6>   $��Center=�6[   .+�5M      '  !MaterialDesignThemes.Wpf.PackIcon  
   Kind$
 	Paperclip=�66   $��20��5N   .   $��20��5O   .   
�������$��0,0,5,0q�5Q   .   �� 5R      $��Seleccionar Imagen��6(   $��Center=�5S       
�������$
��Bold"�5U       5V      5W      5X      5Z      �� 5[   
   -
   5        $
��Left=�5[      $��63��6,   $��856,576,0,0q�68   $	��Top=�6M   $	��180��6e   ���#FF0000��5\      �� 5]      " ��6"   .R�5^      �� #��z��6   $ 20��5_      #��z��5`      $��0q�5a      $ 10q�5b      ._�5c      �� $	 
Horizontal=�6%   $��Center=�6>   $��Center=�6[   .+�5d      �� $��Cancelar��6(   $��Center=�5e      
�������$
��Bold"�5g      $��22(�5h      5i      5j      5k      5m      �� 5n   
   -   5        $
��Left=�5n      $��63��6,   $��1041,576,0,0q�68   $	��Top=�6N   $	��180��6f   ���#008000��5o      �� 5p      " ��6"   .R�5q      �� #��z��6   $ 20��5r      #��z��5s      $��0q�5t      $ 10q�5u      ._�5v      �� $	 
Horizontal=�6%   $��Center=�6>   $��Center=�6[   .+�5w      �� $��Guardar��6(   $��Center=�5x   !   
�������$
��Bold"�5z   !   $��22(�5{   !   5|      5}      5~      5�      �� 5�   
   $
��Left=�6   $��44��6/   $��303,597,0,0q�6;   $
��Wrap=�6P   $	��Top=�6d   $	��419��6|   {�.  �� 5�      $1�+Todos los campos presentes son obligatorios��6   
���   �$��Segoe UI Black)�6X   $��18(�6t    �� 5�      $�*��6   
���  ��$��18(�6,   5�      �� 5�   
   $��Email:��6   $
��Left=�6!   $��38��6<   $��451,542,0,0q�6H   $	��Top=�6]   $��87��6u   $��22(�6�   $��Georgia)�6�   $
��Bold"�6�   5�      5�      