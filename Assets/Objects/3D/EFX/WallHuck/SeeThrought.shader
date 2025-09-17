Shader "FX/SeeThrough" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _OccludeColor ("Occlusion Color", Color) = (0,0,1,1)
    }

    SubShader {
        Tags {"Queue"="Geometry+5"}
        // Основной проход: обработчик рендеринга, когда объект полностью скрыт
        Pass {
            ZWrite Off              // Запись в буфер глубины отключена
            ZTest Greater           // Проверять глубину: показывать только если закрылся
            Cull Back               // Исключаем лицевые полигоны (видим только заднюю сторону)
            Blend One Zero           // Простое наложение цветов
            Color [_OccludeColor]   // Заданный цвет объекта за препятствием
        }
    }
    FallBack "Hidden"
}