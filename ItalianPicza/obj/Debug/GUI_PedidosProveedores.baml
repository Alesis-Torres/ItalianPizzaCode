   M S B A M L   `   `   `  ����G  CItalianPicza, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ItalianPiczaItalianPicza  5      )    #ItalianPicza.GUI_PedidosProveedores   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:ItalianPicza     5        N� Title$  GUI_Usuarios��5	      .��5      � $��12,-4,-12,4q�6   ���	#D3D3D3D3.+�5   
   �� $
��Left=�6   $	��721��6/   
���   �$��Center=�6K   $	��230��6f   ���#231B1B6r   �� 5   
   -   5        $��240,10,922,630q�5      $��80��6i   ��6<   Z� ��6R   Z� .��5      �� $��/Imagenes/Uses/return.png��6   $
��Fill=�67   $��80��6F   $��70��6Q   5      �� 5   
   $
��Left=�6   $��78��6/   $��
484,10,0,0q�6;   $	��Top=�6O   $	��590��6g   ���#231B1B �� RadiusX$ 25��6�    �� RadiusY$ 25��6�   
���  ��$��5��6�   �� 5   
   $!��Revisar Pedidos Proveedores��6   $
��Left=�66   $��44��6Q   $��
570,26,0,0q�6]   $	��Top=�6q   $	��486��6�   
�������$��Georgia)�6�   $��32(�6�   $
��Bold"�6�   �� 5   
   $'��!/Imagenes/Stock/deliveryorder.png��6   $��70��6;   $��70��6G   $
��Left=�6R   $	��Top=�6m   $��
487,13,0,0q�6�   �� 5   
   $
��Left=�6   $��260,109,0,0q�6-    �� TextWrapping$
 Wrap=�6B   $��🔎 Buscar...��6V   $��Georgia)�6j   $��24(�6   $	��Top=�6�   $	��444��6�   $��36��6�   
���   �
�������6�   �� 5   
   -   5        $��lvPedidosProveedores��5      $��258,147,39,133q�6/   
���   ����	#E5000000$��Georgia)�5   6   $��12(�6K   $��1,1,1,1q�6Y    J� SnapsToDevicePixels .   � 	Resources 5       �� 
TargetType �� Property �� Value��%  '��       �� 5      " ��6   .�5      ��  � ��
 �����63   �� 5       � ��
 �   �63   �� 5       � ��$ 16(�61   �� 5        � ��$ Georgia)�63   �� 5!       � ��$
 Bold"�63   �� 5"       � ��$ Center=�6C   5#      5$      ��5%      �� 5&      " ��6   .�5'      ��  � ��
 �����63   �� 5(       � ��
 �   �63   5)      	 �� View	 5,      �� 5-      .P�5/      �� $��Nombre��6%   
 �� Width$	
 200��65    �� CellTemplate 50      �� 51      .R�52   "   �� 
���   �$��Center=�54   $   $��Center=�55   $   $��20(�56   $   $��Georgia)�57   $   ��52   ,   �� *NombrePedidoProveedor+57   $   58      5:      �� 5<      $��Fecha de realización��6%   $	
 180��6C    5=      �� 5>      .R�5?   "   �� 
���   �$��Center=�5A   $   $��Center=�5B   $   $��20(�5C   $   $��Georgia)�5D   $   ��5?   ,   �� *FechaPedidoProveedor+5D   $   5E      5G      �� 5I      $��Costo total��6%   $	
 150��6:    5J      �� 5K      .R�5L   "   �� 
���   �$��Center=�5N   $   $��Center=�5O   $   $��18(�5P   $   $��Georgia)�5Q   $   $
��Wrap=�5R   $   $	��280��5S   $   ��5L   ,   �� *CostoTotalPedidoProveedor+5S   $   5T      5V      �� 5X      $��Estado��6%   $	
 200��65    5Y      �� 5Z      .R�5[   "   �� 
���   �$��Center=�5]   $   $��Center=�5^   $   $��18(�5_   $   $��Georgia)�5`   $   $
��Wrap=�5a   $   $	��280��5b   $   ��5[   ,   �� *EstadoPedidoProveedor+5b   $   5c      5e      �� 5g      $�� ��6%   $	
 230��60    5h      �� 5i      .R�5j   "   ��  �� Orientation$ Vertical=�6-   $��Center=�6D   $��Center=�6a   .+�5k   &   �� -   5        $��✏️ Modificar��5k   ?   $	��200��6V   $��40��6b   $��5,5,5,5q�6n   ���#00008B
�������$��16(�5l   Q   $��Georgia)�5m   )   $
��Bold"�6>   �� 5n   &   -   5        $��👁️ Ver Detalles��5n   A   $	��200��6\   $��40��6h   $��5,5,5,5q�6t   ���#6A5ACD
�������$��16(�5o   Q   $��Georgia)�5p   )   $
��Bold"�6>    � Cursor$  ��6P   5q   #   5r      5t      5v      5x      �� 5z   
   -   5        $��➕ Agregar pedido��5z      $
��Left=�6,   $��775,600,0,0q�6G   $	��Top=�6\   $��62��6t   $	��330��6�   
��� � ����	#FFFCFCFC
�������$��Georgia)�6�   $
��Bold"�6�   $��26(�6  �� 5{   
   -   5        $��✔️ Confirmar entrega��5{      $
��Left=�60   $��442,600,0,0q�6K   $	��Top=�6`   $��62��6x   $	��330��6�   
��� � ����	#FFFCFCFC
�������$��Georgia)�6�   $
��Bold"�6�   $��26(�6  5}      5~      