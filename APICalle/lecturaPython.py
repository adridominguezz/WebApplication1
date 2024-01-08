import xml.etree.ElementTree as ET

fichero = "C:\\Users\\Adri\\Desktop\\WebApplication1\\APICalle\\testpizza.xml"

xmlvar = ET.parse(fichero)
root = xmlvar.getroot()

for pizza_elem in root.findall(".pizza"):
	precio = pizza_elem.get("precio")
	nombre = pizza_elem.get("nombre")

	print(f"Nombre pizza: {nombre}, precio: {precio}.")


	for ingrediente_elem in pizza_elem.findall(".//ingrediente"):
		ingrediente_nombre = ingrediente_elem.get("nombre")
		print(f"- Ingrediente: {ingrediente_nombre}")

	print()
	input("Pulsa para continuar")
