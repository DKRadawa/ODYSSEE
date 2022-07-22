M1=readmatrix('20Hz.csv')      %Importation du fichier csv dans une matrice

Fe=200;                         %Frequence d'echantillonage, frequence de l'accelerometre
Te=1/Fe;
N=length(M1);

t = [0:(N-1)]*Te;               % temps (part de 0, avance de Te en Te, et contient N points


Freq = [0:N-1]/(N*Te);          %l'espace des fréquences

subplot(2,1,1);
plot(t,M1) ;
title('Représentation temporelle');


subplot(2,1,2);
plot(Freq, abs(fft(M1)));       % tracé de l'amplitude en fonction de la fréquence
title('Représentation fréquentielle');



