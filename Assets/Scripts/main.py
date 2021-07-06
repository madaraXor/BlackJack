import catreClass
import joueurClass

def main():
    carte = catreClass.Catre()
    joueur = joueurClass.Joueur()

    joueur.recoisCarte(carte.catrteTir())

    joueur.affichageCarteJoueur()

main()