using System.ComponentModel;

namespace API_DesafioNet.Models
{
    public enum OperacaoModel
    {
        //
        // Resumo:
        // O comando INSERT é usado para adicionar novos registros a uma tabela.
        // O usuário fornece os valores que deseja inserir nas colunas correspondentes da tabela.
        // Se os valores fornecidos estiverem de acordo com as restrições de integridade e tipos
        // de dados da tabela, um novo registro é adicionado à tabela.
        // A sintaxe básica é: INSERT INTO nome_da_tabela (coluna1, coluna2, ...) VALUES (valor1, valor2, ...).
        [Description("I")]
        Insert,
        //
        // Resumo:
        // O comando UPDATE é usado para modificar registros existentes em uma tabela.
        // O usuário especifica quais colunas devem ser atualizadas e quais valores devem ser atribuídos a elas.
        // É comum usar uma cláusula WHERE para filtrar os registros que devem ser atualizados.
        // A sintaxe básica é: UPDATE nome_da_tabela SET coluna1 = novo_valor1, coluna2 = novo_valor2 WHERE condição
        [Description("U")]
        Update,
        //
        // Resumo:
        // O comando DELETE é usado para remover registros de uma tabela.
        // Assim como o UPDATE, pode ser usado em conjunto com uma cláusula WHERE para especificar quais registros devem ser excluídos.
        // Se a cláusula WHERE for omitida, todos os registros da tabela serão excluídos.
        // A sintaxe básica é: DELETE FROM nome_da_tabela WHERE condição
        [Description("D")]
        Delete,
        //
        // Resumo:
        // O comando LIST é usado para Listar os registros de uma tabela.
        // Assim como o UPDATE, pode ser usado em conjunto com uma cláusula WHERE para especificar quais registros devem ser excluídos.
        // Se a cláusula WHERE for omitida, todos os registros da tabela serão excluídos.
        // A sintaxe básica é: SELECT * FROM nome_da_tabela WHERE condição
        [Description("L")]
        List
    }
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());

        var attributes = fieldInfo.GetCustomAttributes(
            typeof(DescriptionAttribute), false) as DescriptionAttribute[];

        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
    }
}
