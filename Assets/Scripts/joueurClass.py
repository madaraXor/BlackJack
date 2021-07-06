class Joueur:

    def __init__(self):
        self.carteDuJoueur = ""

    def recoisCarte(self, value):
        self.carteDuJoueur = value

    def affichageCarteJoueur(self):
        print(self.carteDuJoueur)

