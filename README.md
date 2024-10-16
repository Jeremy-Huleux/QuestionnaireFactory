# Questionnaire Factory

Ce projet, développé dans le cadre de la formation POEC chez Ib Cegos, vise à créer une plateforme permettant la génération automatique de quizz ventilés pour le recrutement de développeurs. L’équipe était composée de **Jérémy Huleux**, **Antoine Martin**, **Yajuan Hu**, et **Aline Simo**. Voici un résumé des fonctionnalités que nous avons pu développer dans le temps imparti (2 semaines).

## Fonctionnalités développées

### 1. Passage de quizz

Nous avons complété la partie permettant aux candidats de passer des quizz. Cette section comprend les éléments suivants :

- Sélection du niveau du quizz (Junior, Confirmé, Expérimenté) selon un système de ventilation prédéfini :
  - Junior : 70% facile, 20% intermédiaire, 10% difficile.
  - Confirmé : 25% facile, 50% intermédiaire, 25% difficile.
  - Expérimenté : 10% facile, 40% intermédiaire, 50% difficile.
- Interface utilisateur intuitive pour la présentation des questions.
- Suivi du score en temps réel et affichage des résultats à la fin du quizz.

### 2. Vue recruteur

La partie dédiée aux recruteurs a également été finalisée, avec les fonctionnalités suivantes :

- Liste des candidats ayant passé les quizz, avec filtres selon les niveaux.
- Visualisation des résultats détaillés des candidats.
- Système de tri et de recherche pour faciliter la gestion des candidats.
- Génération automatique d'un PDF récapitulatif des résultats du candidat.

### 3. Génération de quizz automatique

Nous avons développé un système de génération automatique de quizz ventilés, basé sur un ensemble de critères définis par les recruteurs :

- Génération dynamique de questions selon le niveau choisi (Junior, Confirmé, Expérimenté).
- Gestion de la ventilation des questions pour garantir une difficulté cohérente selon le profil recherché.
- Optimisation des temps de génération des quizz.

### 4. Base de données

Notre base de données, construite avec **T-SQL**, a été conçue pour être modulaire et évolutive. Elle inclut :

- Tables pour stocker les informations des utilisateurs, recruteurs et candidats.
- Tables pour les questions de quizz, ventilées par niveau de difficulté.
- Relation entre les candidats et les quizz passés, permettant une traçabilité complète des résultats.
- Système de gestion des accès et permissions pour les recruteurs.

![Capture d'écran](https://github.com/Jeremy-Huleux/QuestionnaireFactory/blob/master/bdd.png?raw=true)

## Conclusion

Le projet **Questionnaire Factory** nous a permis de développer un système robuste et performant pour la gestion des quizz de recrutement. En deux semaines, nous avons réussi à implémenter les fonctionnalités clés sans rencontrer de crash, tout en respectant les contraintes de temps et les exigences du client.
