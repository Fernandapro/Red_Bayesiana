class Program:
    @staticmethod
    def main():
        red = RedBayesiana()

        # Variables relacionadas con ingredientes
        tieneCarne = True
        tieneEspecias = True

        print("Receta basada en los ingredientes: carne y especias.")
        red.recomendarReceta(tieneCarne, tieneEspecias)


class RedBayesiana:
    # Probabilidad inicial de preparar una receta (tipo de plato)
    probabilidadReceta = {
        "Guiso de Carne": 0.5,
        "Curry Vegetariano": 0.5
    }

    # Probabilidad de usar ingredientes dados ciertas recetas
    probabilidadIngredienteDadaUnaReceta = {
        "Carne": {"Guiso de Carne": 0.9, "Curry Vegetariano": 0.1},
        "Especias": {"Guiso de Carne": 0.6, "Curry Vegetariano": 0.8}
    }

    def inferirProbabilidadReceta(self, receta, tieneCarne, tieneEspecias):
        # Probabilidad inicial de la receta
        probReceta = self.probabilidadReceta[receta]

        # Probabilidad de usar carne y especias dadas las recetas
        probTenerCarne = self.probabilidadIngredienteDadaUnaReceta["Carne"][receta]
        probTenerEspecias = self.probabilidadIngredienteDadaUnaReceta["Especias"][receta]

        # Combina las probabilidades
        unirProb = probReceta
        unirProb *= probTenerCarne if tieneCarne else (1 - probTenerCarne)
        unirProb *= probTenerEspecias if tieneEspecias else (1 - probTenerEspecias)

        return unirProb

    def recomendarReceta(self, tieneCarne, tieneEspecias):
        # Calcula probabilidades para cada receta
        guisoProb = self.inferirProbabilidadReceta("Guiso de Carne", tieneCarne, tieneEspecias)
        curryProb = self.inferirProbabilidadReceta("Curry Vegetariano", tieneCarne, tieneEspecias)

        # Normaliza las probabilidades para que sumen 1 (100%)
        totalProb = guisoProb + curryProb
        guisoProb /= totalProb
        curryProb /= totalProb

        # Imprime el resultado de las recomendaciones
        print(f"Probabilidad de Guiso de Carne: {guisoProb * 100:.2f}%")
        print(f"Probabilidad de Curry Vegetariano: {curryProb * 100:.2f}%")


# Ejecutar el programa
programa = Program()
programa.main()
