import numpy as np
import math
import matplotlib.pyplot as plt

slope = 0.9
waveLimit = 30
enemyLimit = 50

x = np.arange(0, waveLimit / min(1, slope), 0.1)
y = []

for t in x:
    y_1 = int(enemyLimit / (1 + math.exp(-slope / (waveLimit / 10) * (t - (waveLimit / 2))))) + 1
    y.append(y_1)

plt.plot(x, y, label="Sigmoid")
plt.xlabel("Wave Limit")
plt.ylabel("Enemy Limit")
plt.ylim(-1, enemyLimit + 1)
plt.legend()
plt.show()
